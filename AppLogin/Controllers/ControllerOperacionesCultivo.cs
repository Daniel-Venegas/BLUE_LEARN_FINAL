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
    public class ControllerOperacionesCultivo : Controller
    {
        private readonly BLDbContext _context;

        public ControllerOperacionesCultivo(BLDbContext context)
        {
            _context = context;
        }

        // GET: ControllerOperacionesCultivo
        public async Task<IActionResult> Index()
        {
            return View(await _context.OpeCultivos.ToListAsync());
        }

        // GET: ControllerOperacionesCultivo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operacionesCultivo = await _context.OpeCultivos
                .FirstOrDefaultAsync(m => m.IdOperacion == id);
            if (operacionesCultivo == null)
            {
                return NotFound();
            }

            return View(operacionesCultivo);
        }

        // GET: ControllerOperacionesCultivo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControllerOperacionesCultivo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOperacion,IdEstadoOperacion,FechaOperacion,Descripcion,IdCultivo,IdAgricultor,Eliminado")] OperacionesCultivo operacionesCultivo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operacionesCultivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(operacionesCultivo);
        }

        // GET: ControllerOperacionesCultivo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operacionesCultivo = await _context.OpeCultivos.FindAsync(id);
            if (operacionesCultivo == null)
            {
                return NotFound();
            }
            return View(operacionesCultivo);
        }

        // POST: ControllerOperacionesCultivo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOperacion,IdEstadoOperacion,FechaOperacion,Descripcion,IdCultivo,IdAgricultor,Eliminado")] OperacionesCultivo operacionesCultivo)
        {
            if (id != operacionesCultivo.IdOperacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operacionesCultivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperacionesCultivoExists(operacionesCultivo.IdOperacion))
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
            return View(operacionesCultivo);
        }

        // GET: ControllerOperacionesCultivo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operacionesCultivo = await _context.OpeCultivos
                .FirstOrDefaultAsync(m => m.IdOperacion == id);
            if (operacionesCultivo == null)
            {
                return NotFound();
            }

            return View(operacionesCultivo);
        }

        // POST: ControllerOperacionesCultivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operacionesCultivo = await _context.OpeCultivos.FindAsync(id);
            if (operacionesCultivo != null)
            {
                _context.OpeCultivos.Remove(operacionesCultivo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperacionesCultivoExists(int id)
        {
            return _context.OpeCultivos.Any(e => e.IdOperacion == id);
        }
    }
}
