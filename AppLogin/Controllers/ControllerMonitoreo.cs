using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppLogin.Context;
using AppLogin.Model;

namespace AppLogin.Controllers
{
    public class ControllerMonitoreo : Controller
    {
        private readonly BLDbContext _context;

        public ControllerMonitoreo(BLDbContext context)
        {
            _context = context;
        }

        // GET: ControllerMonitoreo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Monitoreo.ToListAsync());
        }

        // GET: ControllerMonitoreo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monitoreo = await _context.Monitoreo
                .FirstOrDefaultAsync(m => m.IdMonitoreo == id);
            if (monitoreo == null)
            {
                return NotFound();
            }

            return View(monitoreo);
        }

        // GET: ControllerMonitoreo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControllerMonitoreo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMonitoreo,FechaMonitoreo,Valor,IdDescripcionMonitoreo,IdCultivo,Eliminado")] Monitoreo monitoreo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monitoreo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monitoreo);
        }

        // GET: ControllerMonitoreo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monitoreo = await _context.Monitoreo.FindAsync(id);
            if (monitoreo == null)
            {
                return NotFound();
            }
            return View(monitoreo);
        }

        // POST: ControllerMonitoreo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMonitoreo,FechaMonitoreo,Valor,IdDescripcionMonitoreo,IdCultivo,Eliminado")] Monitoreo monitoreo)
        {
            if (id != monitoreo.IdMonitoreo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monitoreo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonitoreoExists(monitoreo.IdMonitoreo))
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
            return View(monitoreo);
        }

        // GET: ControllerMonitoreo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monitoreo = await _context.Monitoreo
                .FirstOrDefaultAsync(m => m.IdMonitoreo == id);
            if (monitoreo == null)
            {
                return NotFound();
            }

            return View(monitoreo);
        }

        // POST: ControllerMonitoreo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monitoreo = await _context.Monitoreo.FindAsync(id);
            if (monitoreo != null)
            {
                _context.Monitoreo.Remove(monitoreo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonitoreoExists(int id)
        {
            return _context.Monitoreo.Any(e => e.IdMonitoreo == id);
        }
    }
}
