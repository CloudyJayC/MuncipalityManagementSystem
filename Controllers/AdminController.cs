using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalityManagementSystem.Data;
using MunicipalityManagementSystem.Models;

namespace MunicipalityManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Admin/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            ViewData["TotalUsers"] = await _userManager.Users.CountAsync();
            ViewData["TotalCitizens"] = await _context.Citizens.CountAsync();
            ViewData["TotalStaff"] = await _context.Staffs.CountAsync();
            ViewData["PendingRequests"] = await _context.ServiceRequests.CountAsync(r => r.Status == "Pending");
            ViewData["InProgressRequests"] = await _context.ServiceRequests.CountAsync(r => r.Status == "In Progress");
            ViewData["CompletedRequests"] = await _context.ServiceRequests.CountAsync(r => r.Status == "Completed");
            ViewData["TotalReports"] = await _context.Reports.CountAsync();

            return View();
        }

        // GET: Admin/Users
        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();

            var userViewModels = new List<AdminUserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault() ?? "No Role";

                string? linkedName = null;

                if (role == "Citizen")
                {
                    var citizen = await _context.Citizens
                        .FirstOrDefaultAsync(c => c.UserId == user.Id);
                    linkedName = citizen?.FullName;
                }
                else if (role == "Staff")
                {
                    var staff = await _context.Staffs
                        .FirstOrDefaultAsync(s => s.UserId == user.Id);
                    linkedName = staff?.FullName;
                }

                userViewModels.Add(new AdminUserViewModel
                {
                    Id = user.Id,
                    FullName = $"{user.FirstName} {user.LastName}".Trim(),
                    Email = user.Email ?? string.Empty,
                    Role = role,
                    LinkedName = linkedName
                });
            }

            return View(userViewModels);
        }

        // GET: Admin/CreateStaff
        public IActionResult CreateStaff()
        {
            return View();
        }

        // POST: Admin/CreateStaff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStaff(CreateStaffViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Guard: passwords must match (also validated by [Compare] attribute, but double-check)
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return View(model);
            }

            // Guard: duplicate email
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "An account with this email already exists.");
                return View(model);
            }

            // Create Identity account
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailConfirmed = true   // Admin-created accounts skip email confirmation
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(model);
            }

            // Assign Staff role
            await _userManager.AddToRoleAsync(user, "Staff");

            // Create linked Staff directory entry
            var staff = new Staff
            {
                FullName = $"{model.FirstName} {model.LastName}".Trim(),
                Position = model.Position,
                Department = model.Department,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                HireDate = model.HireDate,
                UserId = user.Id
            };

            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Staff account for {staff.FullName} created successfully.";
            return RedirectToAction(nameof(Users));
        }

        // POST: Admin/DeleteUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                TempData["ErrorMessage"] = "Invalid user ID.";
                return RedirectToAction(nameof(Users));
            }

            // Guard: Admin cannot delete their own account
            var currentUserId = _userManager.GetUserId(User);
            if (id == currentUserId)
            {
                TempData["ErrorMessage"] = "You cannot delete your own account.";
                return RedirectToAction(nameof(Users));
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction(nameof(Users));
            }

            // Detach UserId from Citizens
            var citizens = await _context.Citizens
                .Where(c => c.UserId == id)
                .ToListAsync();
            foreach (var citizen in citizens)
            {
                citizen.UserId = null;
            }

            // Detach UserId from ServiceRequests
            var requests = await _context.ServiceRequests
                .Where(r => r.UserId == id)
                .ToListAsync();
            foreach (var req in requests)
            {
                req.UserId = null;
            }

            // Detach UserId from Staffs
            var staffRecords = await _context.Staffs
                .Where(s => s.UserId == id)
                .ToListAsync();
            foreach (var staffRecord in staffRecords)
            {
                staffRecord.UserId = null;
            }

            await _context.SaveChangesAsync();

            // Delete Identity account
            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                TempData["ErrorMessage"] = "Failed to delete the user account.";
                return RedirectToAction(nameof(Users));
            }

            TempData["SuccessMessage"] = $"User {user.Email} has been deleted.";
            return RedirectToAction(nameof(Users));
        }
    }
}
