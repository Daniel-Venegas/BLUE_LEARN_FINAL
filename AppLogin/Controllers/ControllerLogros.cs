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
    public class ControllerLogros : Controller
    {
        private readonly BLDbContext _context;

        public ControllerLogros(BLDbContext context)
        {
            _context = context;
        }

        // GET: ControllerLogros
        public async Task<IActionResult> Index()
        {
            return View(await _context.Logro.ToListAsync());
        }

        // GET: ControllerLogros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logro = await _context.Logro
                .FirstOrDefaultAsync(m => m.IdLogro == id);
            if (logro == null)
            {
                return NotFound();
            }

            return View(logro);
        }

        // GET: ControllerLogros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControllerLogros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLogro,Descripcion,Fecha,Puntos,Eliminado")] Logro logro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logro);
        }

        // GET: ControllerLogros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logro = await _context.Logro.FindAsync(id);
            if (logro == null)
            {
                return NotFound();
            }
            return View(logro);
        }

        // POST: ControllerLogros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLogro,Descripcion,Fecha,Puntos,Eliminado")] Logro logro)
        {
            if (id != logro.IdLogro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogroExists(logro.IdLogro))
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
            return View(logro);
        }

        // GET: ControllerLogros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logro = await _context.Logro
                .FirstOrDefaultAsync(m => m.IdLogro == id);
            if (logro == null)
            {
                return NotFound();
            }

            return View(logro);
        }

        // POST: ControllerLogros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logro = await _context.Logro.FindAsync(id);
            if (logro != null)
            {
                _context.Logro.Remove(logro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogroExists(int id)
        {
            return _context.Logro.Any(e => e.IdLogro == id);
        }
    }
}
