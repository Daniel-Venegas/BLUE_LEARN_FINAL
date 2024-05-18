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
    public class ControllerEtapaAprendizaje : Controller
    {
        private readonly BLDbContext _context;

        public ControllerEtapaAprendizaje(BLDbContext context)
        {
            _context = context;
        }

        // GET: ControllerEtapaAprendizaje
        public async Task<IActionResult> Index()
        {
            return View(await _context.EtapaAprendizaje.ToListAsync());
        }

        // GET: ControllerEtapaAprendizaje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapaAprendizaje = await _context.EtapaAprendizaje
                .FirstOrDefaultAsync(m => m.IdEstado == id);
            if (etapaAprendizaje == null)
            {
                return NotFound();
            }

            return View(etapaAprendizaje);
        }

        // GET: ControllerEtapaAprendizaje/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControllerEtapaAprendizaje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstado,IdAgricultor,IdEtapa,FechaInit,FechaFin,Eliminado")] EtapaAprendizaje etapaAprendizaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(etapaAprendizaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(etapaAprendizaje);
        }

        // GET: ControllerEtapaAprendizaje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapaAprendizaje = await _context.EtapaAprendizaje.FindAsync(id);
            if (etapaAprendizaje == null)
            {
                return NotFound();
            }
            return View(etapaAprendizaje);
        }

        // POST: ControllerEtapaAprendizaje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstado,IdAgricultor,IdEtapa,FechaInit,FechaFin,Eliminado")] EtapaAprendizaje etapaAprendizaje)
        {
            if (id != etapaAprendizaje.IdEstado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(etapaAprendizaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtapaAprendizajeExists(etapaAprendizaje.IdEstado))
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
            return View(etapaAprendizaje);
        }

        // GET: ControllerEtapaAprendizaje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapaAprendizaje = await _context.EtapaAprendizaje
                .FirstOrDefaultAsync(m => m.IdEstado == id);
            if (etapaAprendizaje == null)
            {
                return NotFound();
            }

            return View(etapaAprendizaje);
        }

        // POST: ControllerEtapaAprendizaje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var etapaAprendizaje = await _context.EtapaAprendizaje.FindAsync(id);
            if (etapaAprendizaje != null)
            {
                _context.EtapaAprendizaje.Remove(etapaAprendizaje);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EtapaAprendizajeExists(int id)
        {
            return _context.EtapaAprendizaje.Any(e => e.IdEstado == id);
        }
    }
}
