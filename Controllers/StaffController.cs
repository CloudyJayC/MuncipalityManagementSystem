using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MunicipalityManagementSystem.Data;
using MunicipalityManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MunicipalityManagementSystem.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Staff
        public async Task<IActionResult> Index()
        {
            return View(await _context.Staffs.ToListAsync());
        }

        // GET: Staff/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .FirstOrDefaultAsync(m => m.StaffID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staff/Create — redirect to Admin portal which handles login account creation too
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            TempData["ErrorMessage"] = "Please use the Admin Portal to create staff accounts — this ensures a login account is created alongside the directory entry.";
            return RedirectToAction("CreateStaff", "Admin");
        }

        // POST: Staff/Create — blocked, use Admin portal
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("FullName,Position,Department,Email,PhoneNumber,HireDate")] Staff staff)
        {
            TempData["ErrorMessage"] = "Please use the Admin Portal to create staff accounts.";
            return RedirectToAction("CreateStaff", "Admin");
        }

        // GET: Staff/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: Staff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("StaffID,FullName,Position,Department,Email,PhoneNumber,HireDate")] Staff staff)
        {
            if (id != staff.StaffID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Preserve existing UserId — never let the edit form wipe it
                    var existing = await _context.Staffs.AsNoTracking()
                        .FirstOrDefaultAsync(s => s.StaffID == staff.StaffID);
                    staff.UserId = existing?.UserId;

                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Staff member updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.StaffID))
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
            return View(staff);
        }

        // GET: Staff/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .FirstOrDefaultAsync(m => m.StaffID == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff != null)
            {
                _context.Staffs.Remove(staff);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Staff member deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Error: Could not delete the staff member.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
            return _context.Staffs.Any(e => e.StaffID == id);
        }
    }
}
