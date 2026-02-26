using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MunicipalityManagementSystem.Data;
using MunicipalityManagementSystem.Models;

namespace MunicipalityManagementSystem.Controllers
{
	public class ReportsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ReportsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Reports
		public async Task<IActionResult> Index()
		{
			var reports = _context.Reports.Include(r => r.Citizen);
			return View(await reports.ToListAsync());
		}

		// GET: Reports/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var report = await _context.Reports
				.Include(r => r.Citizen)
				.FirstOrDefaultAsync(m => m.ReportID == id);
			if (report == null)
			{
				return NotFound();
			}

			return View(report);
		}

		// GET: Reports/Create
		public IActionResult Create()
		{
			ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "FullName");
			return View();
		}

		// POST: Reports/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ReportType,Details,Status,CitizenID")] Report report)
		{
			if (ModelState.IsValid)
			{
				report.SubmissionDate = DateTime.Now;
				_context.Add(report);
				await _context.SaveChangesAsync();

				//Success message
				TempData["SuccessMessage"] = "Report created successfully!";

				return RedirectToAction(nameof(Index));
			}

			// Error message if validation fails
			TempData["ErrorMessage"] = "Error: Could not create report. Please check the form.";

			ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "FullName", report.CitizenID);
			return View(report);
		}

		// GET: Reports/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var report = await _context.Reports.FindAsync(id);
			if (report == null)
			{
				return NotFound();
			}

			ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "FullName", report.CitizenID);
			return View(report);
		}

		// POST: Reports/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ReportID,ReportType,Details,SubmissionDate,Status,CitizenID")] Report report)
		{
			if (id != report.ReportID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(report);
					await _context.SaveChangesAsync();

					// Success message
					TempData["SuccessMessage"] = "Report updated successfully!";
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ReportExists(report.ReportID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}

			TempData["ErrorMessage"] = "Error: Could not update report. Please check the form.";
			ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "FullName", report.CitizenID);
			return View(report);
		}

		// GET: Reports/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var report = await _context.Reports
				.Include(r => r.Citizen)
				.FirstOrDefaultAsync(m => m.ReportID == id);
			if (report == null)
			{
				return NotFound();
			}

			return View(report);
		}

		// POST: Reports/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var report = await _context.Reports.FindAsync(id);
			if (report != null)
			{
				_context.Reports.Remove(report);
				await _context.SaveChangesAsync();

				//Success message
				TempData["SuccessMessage"] = "Report deleted successfully!";
			}
			else
			{
				TempData["ErrorMessage"] = "Error: Could not delete the report.";
			}

			return RedirectToAction(nameof(Index));
		}

		private bool ReportExists(int id)
		{
			return _context.Reports.Any(e => e.ReportID == id);
		}
	}
}
