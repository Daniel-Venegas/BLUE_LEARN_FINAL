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
    public class ControllerCosechas : Controller
    {
        private readonly BLDbContext _context;

        public ControllerCosechas(BLDbContext context)
        {
            _context = context;
        }

        // GET: ControllerCosechas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cosechas.ToListAsync());
        }

        // GET: ControllerCosechas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cosechas = await _context.Cosechas
                .FirstOrDefaultAsync(m => m.IdCosechas == id);
            if (cosechas == null)
            {
                return NotFound();
            }

            return View(cosechas);
        }

        // GET: ControllerCosechas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControllerCosechas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCosechas,FechaCosecha,CantidadRecogida,IdCultivo,IdTemporada,Eliminado")] Cosechas cosechas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cosechas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cosechas);
        }

        // GET: ControllerCosechas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cosechas = await _context.Cosechas.FindAsync(id);
            if (cosechas == null)
            {
                return NotFound();
            }
            return View(cosechas);
        }

        // POST: ControllerCosechas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCosechas,FechaCosecha,CantidadRecogida,IdCultivo,IdTemporada,Eliminado")] Cosechas cosechas)
        {
            if (id != cosechas.IdCosechas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cosechas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CosechasExists(cosechas.IdCosechas))
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
            return View(cosechas);
        }

        // GET: ControllerCosechas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cosechas = await _context.Cosechas
                .FirstOrDefaultAsync(m => m.IdCosechas == id);
            if (cosechas == null)
            {
                return NotFound();
            }

            return View(cosechas);
        }

        // POST: ControllerCosechas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cosechas = await _context.Cosechas.FindAsync(id);
            if (cosechas != null)
            {
                _context.Cosechas.Remove(cosechas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CosechasExists(int id)
        {
            return _context.Cosechas.Any(e => e.IdCosechas == id);
        }
    }
}
