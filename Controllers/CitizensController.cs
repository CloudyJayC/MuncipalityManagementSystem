using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalityManagementSystem.Data;
using MunicipalityManagementSystem.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MunicipalityManagementSystem.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class CitizensController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CitizensController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Citizens
        public async Task<IActionResult> Index()
		{
			return View(await _context.Citizens.ToListAsync());
		}

		// GET: Citizens/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var citizen = await _context.Citizens.FirstOrDefaultAsync(m => m.CitizenID == id);
			if (citizen == null)
			{
				return NotFound();
			}

			return View(citizen);
		}

        // GET: Citizens/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Citizens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("FullName,StreetName,Suburb,City,PostalCode,PhoneNumber,Email,DateOfBirth")] Citizen citizen)
        {
			if (!ModelState.IsValid)
			{
				return View(citizen);
			}

            try
            {
                citizen.RegistrationDate = DateTime.Now;

                // Auto-link UserId if a user account exists with the same email
                if (!string.IsNullOrEmpty(citizen.Email))
                {
                    var existingUser = await _userManager.FindByEmailAsync(citizen.Email);
                    if (existingUser != null)
                    {
                        citizen.UserId = existingUser.Id;
                    }
                }

                _context.Add(citizen);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Citizen added successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
			{
				ModelState.AddModelError("", "An error occurred while saving the data. Please try again.");
				return View(citizen);
			}
		}

		// GET: Citizens/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var citizen = await _context.Citizens.FindAsync(id);
			if (citizen == null)
			{
				return NotFound();
			}
			return View(citizen);
		}

		// POST: Citizens/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("CitizenID,FullName,StreetName,Suburb,City,PostalCode,PhoneNumber,Email,DateOfBirth,RegistrationDate")] Citizen citizen)
		{
			if (id != citizen.CitizenID)
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return View(citizen);
			}

            try
            {
                // Preserve existing UserId — never let the edit form wipe it
                var existingCitizen = await _context.Citizens.AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CitizenID == citizen.CitizenID);

                citizen.UserId = existingCitizen?.UserId;

                // If no UserId is set yet, try to auto-link by email
                if (citizen.UserId == null && !string.IsNullOrEmpty(citizen.Email))
                {
                    var matchedUser = await _userManager.FindByEmailAsync(citizen.Email);
                    if (matchedUser != null)
                    {
                        citizen.UserId = matchedUser.Id;
                    }
                }

                _context.Update(citizen);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Citizen updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
			{
				if (!CitizenExists(citizen.CitizenID))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
		}

        // GET: Citizens/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
			if (id == null)
			{
				return NotFound();
			}

			var citizen = await _context.Citizens.FirstOrDefaultAsync(m => m.CitizenID == id);
			if (citizen == null)
			{
				return NotFound();
			}

			return View(citizen);
		}

        // POST: Citizens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var citizen = await _context.Citizens.FindAsync(id);
            if (citizen != null)
            {
                // If citizen has a linked user account, block deletion
                if (citizen.UserId != null)
                {
                    TempData["ErrorMessage"] = "Cannot delete this citizen — they have a linked user account. Delete their user account first via Admin Portal.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Citizens.Remove(citizen);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Citizen deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Error: Could not delete the citizen.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CitizenExists(int id)
		{
			return _context.Citizens.Any(e => e.CitizenID == id);
		}
	}
}
