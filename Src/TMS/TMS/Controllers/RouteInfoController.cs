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
    public class RouteInfoController : Controller
    {
        private readonly TMSContext _context;

        public RouteInfoController(TMSContext context)
        {
            _context = context;
        }

        // GET: RouteInfo
        public async Task<IActionResult> Index()
        {
              return _context.RouteInfo != null ? 
                          View(await _context.RouteInfo.ToListAsync()) :
                          Problem("Entity set 'TMSContext.RouteInfo'  is null.");
        }

        // GET: RouteInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RouteInfo == null)
            {
                return NotFound();
            }

            var routeInfo = await _context.RouteInfo
                .FirstOrDefaultAsync(m => m.RouteId == id);
            if (routeInfo == null)
            {
                return NotFound();
            }

            return View(routeInfo);
        }

        // GET: RouteInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RouteInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RouteId,RouteStopName1,RouteStopName2,RouteStopName3")] RouteInfo routeInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routeInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(routeInfo);
        }

        // GET: RouteInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RouteInfo == null)
            {
                return NotFound();
            }

            var routeInfo = await _context.RouteInfo.FindAsync(id);
            if (routeInfo == null)
            {
                return NotFound();
            }
            return View(routeInfo);
        }

        // POST: RouteInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RouteId,RouteStopName1,RouteStopName2,RouteStopName3")] RouteInfo routeInfo)
        {
            if (id != routeInfo.RouteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routeInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteInfoExists(routeInfo.RouteId))
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
            return View(routeInfo);
        }

        // GET: RouteInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RouteInfo == null)
            {
                return NotFound();
            }

            var routeInfo = await _context.RouteInfo
                .FirstOrDefaultAsync(m => m.RouteId == id);
            if (routeInfo == null)
            {
                return NotFound();
            }

            return View(routeInfo);
        }

        // POST: RouteInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RouteInfo == null)
            {
                return Problem("Entity set 'TMSContext.RouteInfo'  is null.");
            }
            var routeInfo = await _context.RouteInfo.FindAsync(id);
            if (routeInfo != null)
            {
                _context.RouteInfo.Remove(routeInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteInfoExists(int id)
        {
          return (_context.RouteInfo?.Any(e => e.RouteId == id)).GetValueOrDefault();
        }
    }
}
