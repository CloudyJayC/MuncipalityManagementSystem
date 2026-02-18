using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuncipalityManagementSystem.Data;
using MuncipalityManagementSystem.Models;

namespace MuncipalityManagementSystem.Controllers
{
	public class CitizensController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CitizensController(ApplicationDbContext context)
		{
			_context = context;
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
		public IActionResult Create()
		{
			return View();
		}

		// POST: Citizens/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("FullName,Address,PhoneNumber,Email,DateOfBirth")] Citizen citizen)
		{
			if (!ModelState.IsValid)
			{
				return View(citizen);
			}

			try
			{
				citizen.RegistrationDate = DateTime.Now;
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
		public async Task<IActionResult> Edit(int id, [Bind("CitizenID,FullName,Address,PhoneNumber,Email,DateOfBirth,RegistrationDate")] Citizen citizen)
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
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var citizen = await _context.Citizens.FindAsync(id);
			if (citizen != null)
			{
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
