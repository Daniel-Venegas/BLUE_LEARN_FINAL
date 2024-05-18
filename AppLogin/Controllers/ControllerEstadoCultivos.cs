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
    public class ControllerEstadoCultivos : Controller
    {
        private readonly BLDbContext _context;

        public ControllerEstadoCultivos(BLDbContext context)
        {
            _context = context;
        }

        // GET: ControllerEstadoCultivos
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadoCultivo.ToListAsync());
        }

        // GET: ControllerEstadoCultivos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoCultivo = await _context.EstadoCultivo
                .FirstOrDefaultAsync(m => m.IdEstadoCultivo == id);
            if (estadoCultivo == null)
            {
                return NotFound();
            }

            return View(estadoCultivo);
        }

        // GET: ControllerEstadoCultivos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControllerEstadoCultivos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstadoCultivo,Descripcion,Eliminado")] EstadoCultivo estadoCultivo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadoCultivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadoCultivo);
        }

        // GET: ControllerEstadoCultivos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoCultivo = await _context.EstadoCultivo.FindAsync(id);
            if (estadoCultivo == null)
            {
                return NotFound();
            }
            return View(estadoCultivo);
        }

        // POST: ControllerEstadoCultivos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstadoCultivo,Descripcion,Eliminado")] EstadoCultivo estadoCultivo)
        {
            if (id != estadoCultivo.IdEstadoCultivo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadoCultivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoCultivoExists(estadoCultivo.IdEstadoCultivo))
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
            return View(estadoCultivo);
        }

        // GET: ControllerEstadoCultivos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoCultivo = await _context.EstadoCultivo
                .FirstOrDefaultAsync(m => m.IdEstadoCultivo == id);
            if (estadoCultivo == null)
            {
                return NotFound();
            }

            return View(estadoCultivo);
        }

        // POST: ControllerEstadoCultivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadoCultivo = await _context.EstadoCultivo.FindAsync(id);
            if (estadoCultivo != null)
            {
                _context.EstadoCultivo.Remove(estadoCultivo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoCultivoExists(int id)
        {
            return _context.EstadoCultivo.Any(e => e.IdEstadoCultivo == id);
        }
    }
}
