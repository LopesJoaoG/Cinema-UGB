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
    public class FilmeController : Controller
    {
        private readonly CinemaContext _context;

        public FilmeController(CinemaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Filmes.OrderBy(a => a.Nome).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FilmeId, Nome, Duracao, Genero, IdadeIndicativa")] Filme filme)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(filme);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados.");
            }
            return View(filme);
        }
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var filme = await _context.Filmes.SingleOrDefaultAsync(a => a.FilmeId == id);
            if (filme == null)
            {
                return NotFound();
            }
            return View(filme);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var filme = await _context.Filmes.SingleOrDefaultAsync(a => a.FilmeId == id);
            if (filme == null)
            {
                return NotFound();
            }
            return View(filme);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(long? id, [Bind("FilmeId, Nome, Duracao, Genero, IdadeIndicativa")] Filme filme)
        {
            if(id != filme.FilmeId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmeExists(filme.FilmeId))
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
            return View(filme);
        }
        public bool FilmeExists(long? id)
        {
            return _context.Filmes.Any(x => x.FilmeId == id);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var filme = await _context.Filmes.SingleOrDefaultAsync(a => a.FilmeId == id);
            if (filme == null)
            {
                NotFound();
            }
            return View(filme);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var filme = await _context.Filmes.SingleOrDefaultAsync(a => a.FilmeId == id);
            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
