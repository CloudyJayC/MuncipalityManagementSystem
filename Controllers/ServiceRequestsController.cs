using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MunicipalityManagementSystem.Data;
using MunicipalityManagementSystem.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MunicipalityManagementSystem.Controllers
{
    [Authorize]
    public class ServiceRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServiceRequestsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ServiceRequests
        public async Task<IActionResult> Index()
        {
            var query = _context.ServiceRequests.Include(s => s.Citizen).AsQueryable();

            // Citizens only see their own requests
            if (User.IsInRole("Citizen"))
            {
                var userId = _userManager.GetUserId(User);
                query = query.Where(s => s.UserId == userId);
            }

            return View(await query.ToListAsync());
        }

        // GET: ServiceRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var serviceRequest = await _context.ServiceRequests
                .Include(s => s.Citizen)
                .FirstOrDefaultAsync(m => m.RequestID == id);

            if (serviceRequest == null) return NotFound();

            // Citizens can only view their own requests
            if (User.IsInRole("Citizen"))
            {
                var userId = _userManager.GetUserId(User);
                if (serviceRequest.UserId != userId) return Forbid();
            }

            return View(serviceRequest);
        }

        // GET: ServiceRequests/Create
        public async Task<IActionResult> Create()
        {
            if (User.IsInRole("Citizen"))
            {
                var userId = _userManager.GetUserId(User);
                var citizen = await _context.Citizens.FirstOrDefaultAsync(c => c.UserId == userId);

                if (citizen == null)
                {
                    TempData["ErrorMessage"] = "No citizen profile found for your account.";
                    return RedirectToAction(nameof(Index));
                }

                // Pass CitizenID to view so it can be stored in a hidden field
                ViewData["CitizenID"] = citizen.CitizenID;
                return View();
            }

            // Admin/Staff see the full citizen dropdown
            ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "FullName");
            return View();
        }

        // POST: ServiceRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceRequest serviceRequest)
        {
            // Citizens: override CitizenID and UserId server-side (never trust the form)
            if (User.IsInRole("Citizen"))
            {
                var userId = _userManager.GetUserId(User);
                var citizen = await _context.Citizens.FirstOrDefaultAsync(c => c.UserId == userId);

                if (citizen == null)
                {
                    TempData["ErrorMessage"] = "No citizen profile found for your account.";
                    return RedirectToAction(nameof(Index));
                }

                serviceRequest.CitizenID = citizen.CitizenID;
                serviceRequest.UserId = userId;

                // Clear any validation errors that were raised before we set these
                ModelState.Remove("CitizenID");
                ModelState.Remove("UserId");
            }

            if (!ModelState.IsValid)
            {
                if (User.IsInRole("Citizen"))
                {
                    var userId = _userManager.GetUserId(User);
                    var citizen = await _context.Citizens.FirstOrDefaultAsync(c => c.UserId == userId);
                    ViewData["CitizenID"] = citizen?.CitizenID;
                }
                else
                {
                    ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "FullName", serviceRequest.CitizenID);
                }
                return View(serviceRequest);
            }

            if (serviceRequest.RequestDate == DateTime.MinValue)
                serviceRequest.RequestDate = DateTime.Now;

            // If UserId wasn't set (Admin/Staff creating on behalf of a citizen), look it up
            if (string.IsNullOrEmpty(serviceRequest.UserId))
            {
                var citizen = await _context.Citizens.FirstOrDefaultAsync(c => c.CitizenID == serviceRequest.CitizenID);
                serviceRequest.UserId = citizen?.UserId;
            }

            _context.Add(serviceRequest);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Service request created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: ServiceRequests/Cancel/5  (Citizens only — Pending → Cancelled)
        [Authorize(Roles = "Citizen")]
        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null) return NotFound();

            var serviceRequest = await _context.ServiceRequests
                .Include(s => s.Citizen)
                .FirstOrDefaultAsync(m => m.RequestID == id);

            if (serviceRequest == null) return NotFound();

            var userId = _userManager.GetUserId(User);
            if (serviceRequest.UserId != userId) return Forbid();

            if (serviceRequest.Status != "Pending")
            {
                TempData["ErrorMessage"] = "Only Pending requests can be cancelled.";
                return RedirectToAction(nameof(Index));
            }

            return View(serviceRequest);
        }

        // POST: ServiceRequests/Cancel/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Citizen")]
        public async Task<IActionResult> CancelConfirmed(int id)
        {
            var serviceRequest = await _context.ServiceRequests.FindAsync(id);

            if (serviceRequest == null) return NotFound();

            var userId = _userManager.GetUserId(User);
            if (serviceRequest.UserId != userId) return Forbid();

            if (serviceRequest.Status != "Pending")
            {
                TempData["ErrorMessage"] = "Only Pending requests can be cancelled.";
                return RedirectToAction(nameof(Index));
            }

            serviceRequest.Status = "Cancelled";
            _context.Update(serviceRequest);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Your service request has been cancelled.";
            return RedirectToAction(nameof(Index));
        }

        // GET: ServiceRequests/Edit/5  (Admin/Staff only)
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var serviceRequest = await _context.ServiceRequests.FindAsync(id);
            if (serviceRequest == null) return NotFound();

            ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "FullName", serviceRequest.CitizenID);
            return View(serviceRequest);
        }

        // POST: ServiceRequests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> Edit(int id, ServiceRequest serviceRequest)
        {
            if (id != serviceRequest.RequestID) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "FullName", serviceRequest.CitizenID);
                return View(serviceRequest);
            }

            try
            {
                if (serviceRequest.RequestDate == DateTime.MinValue)
                    serviceRequest.RequestDate = DateTime.Now;

                // Preserve existing UserId — never let the edit form wipe it
                var existing = await _context.ServiceRequests.AsNoTracking()
                    .FirstOrDefaultAsync(s => s.RequestID == serviceRequest.RequestID);
                serviceRequest.UserId = existing?.UserId;

                _context.Update(serviceRequest);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Service request updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceRequestExists(serviceRequest.RequestID)) return NotFound();
                throw;
            }
        }

        // GET: ServiceRequests/Delete/5  (Admin only)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var serviceRequest = await _context.ServiceRequests
                .Include(s => s.Citizen)
                .FirstOrDefaultAsync(m => m.RequestID == id);

            if (serviceRequest == null) return NotFound();

            return View(serviceRequest);
        }

        // POST: ServiceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceRequest = await _context.ServiceRequests.FindAsync(id);
            if (serviceRequest != null)
            {
                _context.ServiceRequests.Remove(serviceRequest);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Service request deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceRequestExists(int id)
        {
            return _context.ServiceRequests.Any(e => e.RequestID == id);
        }
    }
}
