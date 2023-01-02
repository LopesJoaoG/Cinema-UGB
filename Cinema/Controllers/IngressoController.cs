using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class IngressoController : Controller
    {
        private readonly CinemaContext _context;

        public IngressoController(CinemaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var CinemaUGBContext = _context.Ingressos.Include(p => p.Sessao);
            return View(await CinemaUGBContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["SessaoId"] = new SelectList(_context.Sessaos, "SessaoId", "Numero");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IngressoId, Preco, SessaoId")] Ingresso ingresso)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(ingresso);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados.");
            }
            ViewData["SessaoId"] = new SelectList(_context.Sessaos, "SessaoId", "Numero");
            return View(ingresso);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ingresso = await _context.Ingressos.SingleOrDefaultAsync(a => a.IngressoId == id);
            if (ingresso == null)
            {
                return NotFound();
            }
            return View(ingresso);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var filme = await _context.Ingressos.SingleOrDefaultAsync(a => a.IngressoId == id);
            if (filme == null)
            {
                return NotFound();
            }
            return View(filme);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(long? id, [Bind("IngressoId, Preco, SessaoId")] Ingresso ingresso)
        {
            if (id != ingresso.IngressoId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingresso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngressoExists(ingresso.IngressoId))
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
            return View(ingresso);
        }
        public bool IngressoExists(long? id)
        {
            return _context.Ingressos.Any(x => x.IngressoId == id);
        }
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ingresso = await _context.Ingressos.SingleOrDefaultAsync(a => a.IngressoId == id);
            if (ingresso == null)
            {
                NotFound();
            }
            return View(ingresso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var ingresso = await _context.Ingressos.SingleOrDefaultAsync(a => a.IngressoId == id);
            _context.Ingressos.Remove(ingresso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
