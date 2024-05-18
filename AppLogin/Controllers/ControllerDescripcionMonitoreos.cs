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
    public class ControllerDescripcionMonitoreos : Controller
    {
        private readonly BLDbContext _context;

        public ControllerDescripcionMonitoreos(BLDbContext context)
        {
            _context = context;
        }

        // GET: ControllerDescripcionMonitoreos
        public async Task<IActionResult> Index()
        {
            return View(await _context.DescripcionMonitoreo.ToListAsync());
        }

        // GET: ControllerDescripcionMonitoreos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descripcionMonitoreo = await _context.DescripcionMonitoreo
                .FirstOrDefaultAsync(m => m.IdDescripcionMonitoreo == id);
            if (descripcionMonitoreo == null)
            {
                return NotFound();
            }

            return View(descripcionMonitoreo);
        }

        // GET: ControllerDescripcionMonitoreos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControllerDescripcionMonitoreos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDescripcionMonitoreo,Variable,UnidadMedida,Eliminado")] DescripcionMonitoreo descripcionMonitoreo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(descripcionMonitoreo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(descripcionMonitoreo);
        }

        // GET: ControllerDescripcionMonitoreos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descripcionMonitoreo = await _context.DescripcionMonitoreo.FindAsync(id);
            if (descripcionMonitoreo == null)
            {
                return NotFound();
            }
            return View(descripcionMonitoreo);
        }

        // POST: ControllerDescripcionMonitoreos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDescripcionMonitoreo,Variable,UnidadMedida,Eliminado")] DescripcionMonitoreo descripcionMonitoreo)
        {
            if (id != descripcionMonitoreo.IdDescripcionMonitoreo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(descripcionMonitoreo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescripcionMonitoreoExists(descripcionMonitoreo.IdDescripcionMonitoreo))
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
            return View(descripcionMonitoreo);
        }

        // GET: ControllerDescripcionMonitoreos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var descripcionMonitoreo = await _context.DescripcionMonitoreo
                .FirstOrDefaultAsync(m => m.IdDescripcionMonitoreo == id);
            if (descripcionMonitoreo == null)
            {
                return NotFound();
            }

            return View(descripcionMonitoreo);
        }

        // POST: ControllerDescripcionMonitoreos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var descripcionMonitoreo = await _context.DescripcionMonitoreo.FindAsync(id);
            if (descripcionMonitoreo != null)
            {
                _context.DescripcionMonitoreo.Remove(descripcionMonitoreo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescripcionMonitoreoExists(int id)
        {
            return _context.DescripcionMonitoreo.Any(e => e.IdDescripcionMonitoreo == id);
        }
    }
}
