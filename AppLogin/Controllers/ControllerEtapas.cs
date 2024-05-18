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
    public class ControllerEtapas : Controller
    {
        private readonly BLDbContext _context;

        public ControllerEtapas(BLDbContext context)
        {
            _context = context;
        }

        // GET: ControllerEtapas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Etapa.ToListAsync());
        }

        // GET: ControllerEtapas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapa = await _context.Etapa
                .FirstOrDefaultAsync(m => m.IdEtapa == id);
            if (etapa == null)
            {
                return NotFound();
            }

            return View(etapa);
        }

        // GET: ControllerEtapas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControllerEtapas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEtapa,Descripcion,Eliminado")] Etapa etapa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(etapa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(etapa);
        }

        // GET: ControllerEtapas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapa = await _context.Etapa.FindAsync(id);
            if (etapa == null)
            {
                return NotFound();
            }
            return View(etapa);
        }

        // POST: ControllerEtapas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEtapa,Descripcion,Eliminado")] Etapa etapa)
        {
            if (id != etapa.IdEtapa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(etapa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtapaExists(etapa.IdEtapa))
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
            return View(etapa);
        }

        // GET: ControllerEtapas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapa = await _context.Etapa
                .FirstOrDefaultAsync(m => m.IdEtapa == id);
            if (etapa == null)
            {
                return NotFound();
            }

            return View(etapa);
        }

        // POST: ControllerEtapas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var etapa = await _context.Etapa.FindAsync(id);
            if (etapa != null)
            {
                _context.Etapa.Remove(etapa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EtapaExists(int id)
        {
            return _context.Etapa.Any(e => e.IdEtapa == id);
        }
    }
}
