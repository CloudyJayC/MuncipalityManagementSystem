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
            var userId = _userManager.GetUserId(User);

            // Pull name from Staff directory record if available, fall back to Identity name
            var staffRecord = await _context.Staffs.FirstOrDefaultAsync(s => s.UserId == userId);
            if (staffRecord != null)
            {
                ViewData["StaffName"] = staffRecord.FullName;
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                ViewData["StaffName"] = $"{user?.FirstName} {user?.LastName}";
            }

            // Stat cards
            ViewData["TotalCitizens"] = await _context.Citizens.CountAsync();
            ViewData["PendingCount"] = await _context.ServiceRequests.CountAsync(r => r.Status == "Pending");
            ViewData["InProgressCount"] = await _context.ServiceRequests.CountAsync(r => r.Status == "In Progress");
            ViewData["CompletedCount"] = await _context.ServiceRequests.CountAsync(r => r.Status == "Completed");
            ViewData["TotalReports"] = await _context.Reports.CountAsync();

            // 5 most recent service requests for the recent activity table
            var recentRequests = await _context.ServiceRequests
                .Include(r => r.Citizen)
                .OrderByDescending(r => r.RequestDate)
                .Take(5)
                .ToListAsync();

            // Line chart — requests created per day for the last 7 days
            var today = DateTime.Today;
            var sevenDaysAgo = today.AddDays(-6);

            var requestsLast7Days = await _context.ServiceRequests
                .Where(r => r.RequestDate.Date >= sevenDaysAgo)
                .GroupBy(r => r.RequestDate.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToListAsync();

            // Build a complete 7-day series — fill 0 for days with no requests
            var chartLabels = new List<string>();
            var chartCounts = new List<int>();

            for (int i = 6; i >= 0; i--)
            {
                var day = today.AddDays(-i);
                chartLabels.Add(day.ToString("dd MMM"));
                var match = requestsLast7Days.FirstOrDefault(x => x.Date == day);
                chartCounts.Add(match?.Count ?? 0);
            }

            ViewData["LineChartLabels"] = System.Text.Json.JsonSerializer.Serialize(chartLabels);
            ViewData["LineChartCounts"] = System.Text.Json.JsonSerializer.Serialize(chartCounts);

            return View(recentRequests);
        }
    }
}
