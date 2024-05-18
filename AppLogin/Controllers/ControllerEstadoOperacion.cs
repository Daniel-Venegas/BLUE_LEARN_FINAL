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
    public class ControllerEstadoOperacion : Controller
    {
        private readonly BLDbContext _context;

        public ControllerEstadoOperacion(BLDbContext context)
        {
            _context = context;
        }

        // GET: ControllerEstadoOperacion
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadoOperacion.ToListAsync());
        }

        // GET: ControllerEstadoOperacion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoOperacion = await _context.EstadoOperacion
                .FirstOrDefaultAsync(m => m.IdEstadoOperacion == id);
            if (estadoOperacion == null)
            {
                return NotFound();
            }

            return View(estadoOperacion);
        }

        // GET: ControllerEstadoOperacion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControllerEstadoOperacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstadoOperacion,Descripcion,Eliminado")] EstadoOperacion estadoOperacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadoOperacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadoOperacion);
        }

        // GET: ControllerEstadoOperacion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoOperacion = await _context.EstadoOperacion.FindAsync(id);
            if (estadoOperacion == null)
            {
                return NotFound();
            }
            return View(estadoOperacion);
        }

        // POST: ControllerEstadoOperacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstadoOperacion,Descripcion,Eliminado")] EstadoOperacion estadoOperacion)
        {
            if (id != estadoOperacion.IdEstadoOperacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadoOperacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoOperacionExists(estadoOperacion.IdEstadoOperacion))
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
            return View(estadoOperacion);
        }

        // GET: ControllerEstadoOperacion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoOperacion = await _context.EstadoOperacion
                .FirstOrDefaultAsync(m => m.IdEstadoOperacion == id);
            if (estadoOperacion == null)
            {
                return NotFound();
            }

            return View(estadoOperacion);
        }

        // POST: ControllerEstadoOperacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadoOperacion = await _context.EstadoOperacion.FindAsync(id);
            if (estadoOperacion != null)
            {
                _context.EstadoOperacion.Remove(estadoOperacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoOperacionExists(int id)
        {
            return _context.EstadoOperacion.Any(e => e.IdEstadoOperacion == id);
        }
    }
}
