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
    public class ControllerTemporadas : Controller
    {
        private readonly BLDbContext _context;

        public ControllerTemporadas(BLDbContext context)
        {
            _context = context;
        }

        // GET: ControllerTemporadas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Temporadas.ToListAsync());
        }

        // GET: ControllerTemporadas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temporadas = await _context.Temporadas
                .FirstOrDefaultAsync(m => m.IdTemporada == id);
            if (temporadas == null)
            {
                return NotFound();
            }

            return View(temporadas);
        }

        // GET: ControllerTemporadas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControllerTemporadas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTemporada,Temporada,Eliminado")] Temporadas temporadas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(temporadas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(temporadas);
        }

        // GET: ControllerTemporadas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temporadas = await _context.Temporadas.FindAsync(id);
            if (temporadas == null)
            {
                return NotFound();
            }
            return View(temporadas);
        }

        // POST: ControllerTemporadas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTemporada,Temporada,Eliminado")] Temporadas temporadas)
        {
            if (id != temporadas.IdTemporada)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(temporadas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemporadasExists(temporadas.IdTemporada))
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
            return View(temporadas);
        }

        // GET: ControllerTemporadas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temporadas = await _context.Temporadas
                .FirstOrDefaultAsync(m => m.IdTemporada == id);
            if (temporadas == null)
            {
                return NotFound();
            }

            return View(temporadas);
        }

        // POST: ControllerTemporadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var temporadas = await _context.Temporadas.FindAsync(id);
            if (temporadas != null)
            {
                _context.Temporadas.Remove(temporadas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemporadasExists(int id)
        {
            return _context.Temporadas.Any(e => e.IdTemporada == id);
        }
    }
}
