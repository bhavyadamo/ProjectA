using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMS.Data;
using TMS.Models;

namespace TMS.Controllers
{
    public class EmployeeInfoController : Controller
    {
        private readonly TMSContext _context;

        public EmployeeInfoController(TMSContext context)
        {
            _context = context;
        }

        // GET: EmployeeInfo
        public async Task<IActionResult> Index()
        {
            var tMSContext = _context.EmployeeInfo.Include(e => e.RouteInfo);
            return View(await tMSContext.ToListAsync());
        }

        // GET: EmployeeInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeInfo == null)
            {
                return NotFound();
            }

            var employeeInfo = await _context.EmployeeInfo
                .Include(e => e.RouteInfo)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employeeInfo == null)
            {
                return NotFound();
            }

            return View(employeeInfo);
        }

        // GET: EmployeeInfo/Create
        public IActionResult Create()
        {
            ViewData["RouteId"] = new SelectList(_context.RouteInfo, "RouteId", "RouteId");
            return View();
        }

        // POST: EmployeeInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,EmpName,Age,Location,Phone,RouteId")] EmployeeInfo employeeInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RouteId"] = new SelectList(_context.RouteInfo, "RouteId", "RouteId", employeeInfo.RouteId);
            return View(employeeInfo);
        }

        // GET: EmployeeInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeInfo == null)
            {
                return NotFound();
            }

            var employeeInfo = await _context.EmployeeInfo.FindAsync(id);
            if (employeeInfo == null)
            {
                return NotFound();
            }
            ViewData["RouteId"] = new SelectList(_context.RouteInfo, "RouteId", "RouteId", employeeInfo.RouteId);
            return View(employeeInfo);
        }

        // POST: EmployeeInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpId,EmpName,Age,Location,Phone,RouteId")] EmployeeInfo employeeInfo)
        {
            if (id != employeeInfo.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeInfoExists(employeeInfo.EmpId))
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
            ViewData["RouteId"] = new SelectList(_context.RouteInfo, "RouteId", "RouteId", employeeInfo.RouteId);
            return View(employeeInfo);
        }

        // GET: EmployeeInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeInfo == null)
            {
                return NotFound();
            }

            var employeeInfo = await _context.EmployeeInfo
                .Include(e => e.RouteInfo)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employeeInfo == null)
            {
                return NotFound();
            }

            return View(employeeInfo);
        }

        // POST: EmployeeInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeInfo == null)
            {
                return Problem("Entity set 'TMSContext.EmployeeInfo'  is null.");
            }
            var employeeInfo = await _context.EmployeeInfo.FindAsync(id);
            if (employeeInfo != null)
            {
                _context.EmployeeInfo.Remove(employeeInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeInfoExists(int id)
        {
          return (_context.EmployeeInfo?.Any(e => e.EmpId == id)).GetValueOrDefault();
        }
    }
}
