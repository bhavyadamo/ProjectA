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
    public class AllocateController : Controller
    {
        private readonly TMSContext _context;

        public const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=TMSContext-a6297cd2-3b36-4e69-bceb-395522eb286e;Trusted_Connection=True";
        protected void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(connectionString);
        }

        public AllocateController(TMSContext context)
        {
            _context = context;
        }

        // GET: Allocate
        public async Task<IActionResult> Index()
        {
            var tMSContext = _context.Allocate.Include(a => a.EmployeeInfo).Include(a => a.RouteInfo).Include(a => a.VehicleInfo);
            return View(await tMSContext.ToListAsync());
        }

        // GET: Allocate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Allocate == null)
            {
                return NotFound();
            }

            var allocate = await _context.Allocate
                .Include(a => a.EmployeeInfo)
                .Include(a => a.RouteInfo)
                .Include(a => a.VehicleInfo)
                .FirstOrDefaultAsync(m => m.AllocateId == id);
            if (allocate == null)
            {
                return NotFound();
            }

            return View(allocate);
        }
        public async Task<IActionResult> UpdateSeat(string a)
        {
            var allocate = _context.Allocate;
            using (var dbconnect = _context)
            {

                VehicleInfo? l = dbconnect.VehicleInfo.Find(a);
                int b = l.AvailSeats;
                l.AvailSeats = b - 1;
                dbconnect.SaveChangesAsync();
                return View(allocate);
            }
        }

        // GET: Allocate/Create
        public IActionResult Create()
        {
            ViewData["EmpId"] = new SelectList(_context.EmployeeInfo, "EmpId", "EmpId");
            ViewData["RouteId"] = new SelectList(_context.RouteInfo, "RouteId", "RouteId");
            ViewData["VehicleId"] = new SelectList(_context.VehicleInfo, "VehicleId", "VehicleId");
            return View();
        }

        // POST: Allocate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AllocateId,VehicleId,EmpId,RouteId")] Allocate allocate)
        {
            var a = allocate.VehicleId;
            if (ModelState.IsValid)
            {
                _context.Add(allocate);
                await _context.SaveChangesAsync();
                await UpdateSeat(a);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpId"] = new SelectList(_context.EmployeeInfo, "EmpId", "EmpId", allocate.EmpId);
            ViewData["RouteId"] = new SelectList(_context.RouteInfo, "RouteId", "RouteId", allocate.RouteId);
            ViewData["VehicleId"] = new SelectList(_context.VehicleInfo, "VehicleId", "VehicleId", allocate.VehicleId);
            return View(allocate);
        }

        // GET: Allocate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Allocate == null)
            {
                return NotFound();
            }

            var allocate = await _context.Allocate.FindAsync(id);
            if (allocate == null)
            {
                return NotFound();
            }
            ViewData["EmpId"] = new SelectList(_context.EmployeeInfo, "EmpId", "EmpId", allocate.EmpId);
            ViewData["RouteId"] = new SelectList(_context.RouteInfo, "RouteId", "RouteId", allocate.RouteId);
            ViewData["VehicleId"] = new SelectList(_context.VehicleInfo, "VehicleId", "VehicleId", allocate.VehicleId);
            return View(allocate);
        }

        // POST: Allocate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AllocateId,VehicleId,EmpId,RouteId")] Allocate allocate)
        {
            if (id != allocate.AllocateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allocate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllocateExists(allocate.AllocateId))
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
            ViewData["EmpId"] = new SelectList(_context.EmployeeInfo, "EmpId", "EmpId", allocate.EmpId);
            ViewData["RouteId"] = new SelectList(_context.RouteInfo, "RouteId", "RouteId", allocate.RouteId);
            ViewData["VehicleId"] = new SelectList(_context.VehicleInfo, "VehicleId", "VehicleId", allocate.VehicleId);
            return View(allocate);
        }

        // GET: Allocate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Allocate == null)
            {
                return NotFound();
            }

            var allocate = await _context.Allocate
                .Include(a => a.EmployeeInfo)
                .Include(a => a.RouteInfo)
                .Include(a => a.VehicleInfo)
                .FirstOrDefaultAsync(m => m.AllocateId == id);
            if (allocate == null)
            {
                return NotFound();
            }

            return View(allocate);
        }

        // POST: Allocate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Allocate == null)
            {
                return Problem("Entity set 'TMSContext.Allocate'  is null.");
            }
            var allocate = await _context.Allocate.FindAsync(id);
            if (allocate != null)
            {
                _context.Allocate.Remove(allocate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllocateExists(int id)
        {
          return (_context.Allocate?.Any(e => e.AllocateId == id)).GetValueOrDefault();
        }
    }
}
