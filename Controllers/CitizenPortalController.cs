using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalityManagementSystem.Data;
using MunicipalityManagementSystem.Models;
using System.Threading.Tasks;

namespace MunicipalityManagementSystem.Controllers
{
    [Authorize(Roles = "Citizen")]
    public class CitizenPortalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CitizenPortalController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: CitizenPortal/Profile
        public async Task<IActionResult> Profile()
        {
            var userId = _userManager.GetUserId(User);
            var citizen = await _context.Citizens.FirstOrDefaultAsync(c => c.UserId == userId);

            if (citizen == null)
            {
                TempData["ErrorMessage"] = "No citizen profile found for your account.";
                return RedirectToAction("Index", "Home");
            }

            // Mini stat strip — scoped to this citizen only
            ViewData["TotalRequests"] = await _context.ServiceRequests
                .CountAsync(r => r.CitizenID == citizen.CitizenID);
            ViewData["PendingRequests"] = await _context.ServiceRequests
                .CountAsync(r => r.CitizenID == citizen.CitizenID && r.Status == "Pending");
            ViewData["CompletedRequests"] = await _context.ServiceRequests
                .CountAsync(r => r.CitizenID == citizen.CitizenID && r.Status == "Completed");

            return View(citizen);
        }

        // POST: CitizenPortal/Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(int CitizenID, string streetName, string suburb, string city, string postalCode, string phoneNumber)
        {
            var userId = _userManager.GetUserId(User);
            var citizen = await _context.Citizens.FirstOrDefaultAsync(c => c.UserId == userId);

            if (citizen == null || citizen.CitizenID != CitizenID)
                return Forbid();

            // Only update the editable fields
            citizen.StreetName = streetName;
            citizen.Suburb = suburb;
            citizen.City = city;
            citizen.PostalCode = postalCode;
            citizen.PhoneNumber = phoneNumber;

            _context.Update(citizen);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your profile has been updated successfully.";
            return RedirectToAction(nameof(Profile));
        }

        // GET: CitizenPortal/DeleteAccount
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = _userManager.GetUserId(User);
            var citizen = await _context.Citizens.FirstOrDefaultAsync(c => c.UserId == userId);

            if (citizen == null)
            {
                TempData["ErrorMessage"] = "No citizen profile found for your account.";
                return RedirectToAction("Index", "Home");
            }

            return View(citizen);
        }

        // POST: CitizenPortal/DeleteAccount
        [HttpPost, ActionName("DeleteAccount")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccountConfirmed()
        {
            var userId = _userManager.GetUserId(User) ?? string.Empty;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound();

            // Detach UserId from citizen record (keep the citizen record intact)
            var citizen = await _context.Citizens.FirstOrDefaultAsync(c => c.UserId == userId);
            if (citizen != null)
            {
                citizen.UserId = null;
                _context.Update(citizen);
            }

            // Detach UserId from all their service requests
            var requests = await _context.ServiceRequests
                .Where(s => s.UserId == userId)
                .ToListAsync();

            foreach (var request in requests)
            {
                request.UserId = null;
                _context.Update(request);
            }

            await _context.SaveChangesAsync();

            // Sign out and delete the Identity account
            await _signInManager.SignOutAsync();
            await _userManager.DeleteAsync(user);

            TempData["SuccessMessage"] = "Your account has been deleted.";
            return RedirectToAction("Index", "Home");
        }
    }
}
