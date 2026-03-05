using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalityManagementSystem.Data;
using MunicipalityManagementSystem.Models;

namespace MunicipalityManagementSystem.Controllers
{
    [Authorize(Roles = "Staff")]
    public class StaffPortalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StaffPortalController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: StaffPortal/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);

            ViewData["StaffName"] = $"{user?.FirstName} {user?.LastName}";
            ViewData["PendingCount"] = await _context.ServiceRequests.CountAsync(s => s.Status == "Pending");
            ViewData["InProgressCount"] = await _context.ServiceRequests.CountAsync(s => s.Status == "In Progress");
            ViewData["CompletedCount"] = await _context.ServiceRequests.CountAsync(s => s.Status == "Completed");
            ViewData["TotalCitizens"] = await _context.Citizens.CountAsync();
            ViewData["TotalReports"] = await _context.Reports.CountAsync();

            // 5 most recent service requests
            var recentRequests = await _context.ServiceRequests
                .Include(s => s.Citizen)
                .OrderByDescending(s => s.RequestDate)
                .Take(5)
                .ToListAsync();

            return View(recentRequests);
        }
    }
}
