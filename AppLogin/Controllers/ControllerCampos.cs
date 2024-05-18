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
    public class ControllerCampos : Controller
    {
        private readonly BLDbContext _context;

        public ControllerCampos(BLDbContext context)
        {
            _context = context;
        }

        // GET: ControllerCampos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Campos.ToListAsync());
        }

        // GET: ControllerCampos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campos = await _context.Campos
                .FirstOrDefaultAsync(m => m.IdCampo == id);
            if (campos == null)
            {
                return NotFound();
            }

            return View(campos);
        }

        // GET: ControllerCampos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ControllerCampos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCampo,NombreCampo,Ubicacion,Tamano,Eliminado")] Campos campos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campos);
        }

        // GET: ControllerCampos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campos = await _context.Campos.FindAsync(id);
            if (campos == null)
            {
                return NotFound();
            }
            return View(campos);
        }

        // POST: ControllerCampos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCampo,NombreCampo,Ubicacion,Tamano,Eliminado")] Campos campos)
        {
            if (id != campos.IdCampo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CamposExists(campos.IdCampo))
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
            return View(campos);
        }

        // GET: ControllerCampos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campos = await _context.Campos
                .FirstOrDefaultAsync(m => m.IdCampo == id);
            if (campos == null)
            {
                return NotFound();
            }

            return View(campos);
        }

        // POST: ControllerCampos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campos = await _context.Campos.FindAsync(id);
            if (campos != null)
            {
                _context.Campos.Remove(campos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CamposExists(int id)
        {
            return _context.Campos.Any(e => e.IdCampo == id);
        }
    }
}
