using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MuncipalityManagementSystem.Data;
using MuncipalityManagementSystem.Models;

namespace MuncipalityManagementSystem.Controllers
{
	public class ServiceRequestsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ServiceRequestsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: ServiceRequests
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.ServiceRequests.Include(s => s.Citizen);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: ServiceRequests/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null) return NotFound();

			var serviceRequest = await _context.ServiceRequests
				.Include(s => s.Citizen)
				.FirstOrDefaultAsync(m => m.RequestID == id);

			if (serviceRequest == null) return NotFound();

			return View(serviceRequest);
		}

		// GET: ServiceRequests/Create
		public IActionResult Create()
		{
			ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "FullName");
			return View();
		}

		// POST: ServiceRequests/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ServiceRequest serviceRequest)
		{
			if (!ModelState.IsValid)
			{
				// Capture validation errors and display them in the view
				foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
				{
					ModelState.AddModelError("", error.ErrorMessage);
				}

				ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "FullName", serviceRequest.CitizenID);
				return View(serviceRequest);
			}

			try
			{
				// Ensure RequestDate is properly set
				if (serviceRequest.RequestDate == null || serviceRequest.RequestDate == DateTime.MinValue)
				{
					serviceRequest.RequestDate = DateTime.Now;
				}

				_context.Add(serviceRequest);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				// Log the error and show it in the UI
				ModelState.AddModelError("", "An error occurred while saving the request: " + ex.Message);
				ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "FullName", serviceRequest.CitizenID);
				return View(serviceRequest);
			}
		}

		// GET: ServiceRequests/Edit/5
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
				// Ensure RequestDate is properly set
				if (serviceRequest.RequestDate == null || serviceRequest.RequestDate == DateTime.MinValue)
				{
					serviceRequest.RequestDate = DateTime.Now;
				}

				_context.Update(serviceRequest);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ServiceRequestExists(serviceRequest.RequestID))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "An error occurred while updating the request: " + ex.Message);
				ViewData["CitizenID"] = new SelectList(_context.Citizens, "CitizenID", "FullName", serviceRequest.CitizenID);
				return View(serviceRequest);
			}
		}

		// Helper method to check if a service request exists
		private bool ServiceRequestExists(int id)
		{
			return _context.ServiceRequests.Any(e => e.RequestID == id);
		}
	}
}
