using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class SalaController : Controller
    {
        private readonly CinemaContext _context;

        public SalaController(CinemaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Salas.OrderBy(a => a.SalaId).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalaId, Numero, Poltrona")] Sala sala)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(sala);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados.");
            }
            return View(sala);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sala = await _context.Salas.SingleOrDefaultAsync(a => a.SalaId == id);
            if (sala == null)
            {
                return NotFound();
            }
            return View(sala);
        }
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sala = await _context.Salas.SingleOrDefaultAsync(a => a.SalaId == id);
            if (sala == null)
            {
                return NotFound();
            }
            return View(sala);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(long? id, [Bind("SalaId, Numero, Poltrona")] Sala sala)
        {
            if (id != sala.SalaId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaExists(sala.SalaId))
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
            return View(sala);
        }
        public bool SalaExists(long? id)
        {
            return _context.Salas.Any(x => x.SalaId == id);
        }
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sala = await _context.Salas.SingleOrDefaultAsync(a => a.SalaId == id);
            if (sala == null)
            {
                NotFound();
            }
            return View(sala);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var sala = await _context.Salas.SingleOrDefaultAsync(a => a.SalaId == id);
            _context.Salas.Remove(sala);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
