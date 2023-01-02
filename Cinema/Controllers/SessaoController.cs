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
    public class SessaoController : Controller
    {
        private readonly CinemaContext _context;

        public SessaoController(CinemaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var CinemaUGBContext = _context.Sessaos.Include(p => p.Filme).Include(p => p.Sala);
            return View( await CinemaUGBContext.ToListAsync()) ;
        }

        public IActionResult Create()
        {
            ViewData["FilmeId"] = new SelectList(_context.Filmes, "FilmeId", "Nome");
            ViewData["SalaId"] = new SelectList(_context.Salas, "SalaId", "Numero");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SessaoId, DataHorario, Vagas, FilmeId, SalaId, Numero")] Sessao sessao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(sessao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir dados.");
            }
            ViewData["FilmeId"] = new SelectList(_context.Filmes, "FilmeId", "Nome");
            ViewData["SalaId"] = new SelectList(_context.Salas, "SalaId", "Numero");
            return View(sessao);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sessao = await _context.Sessaos.Include(a => a.Filme).Include(a => a.Sala).SingleOrDefaultAsync(a => a.SessaoId == id);
            if (sessao == null)
            {
                return NotFound();
            }
            return View(sessao);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sessao = await _context.Sessaos.SingleOrDefaultAsync(a => a.SessaoId == id);
            if (sessao == null)
            {
                return NotFound();
            }
            ViewData["FilmeId"] = new SelectList(_context.Filmes, "FilmeId", "Nome");
            ViewData["SalaId"] = new SelectList(_context.Salas, "SalaId", "Numero");
            return View(sessao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(long? id, [Bind("SessaoId, DataHorario, Vagas, FilmeId, SalaId, Numero")] Sessao sessao)
        {
            if (id != sessao.SessaoId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sessao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessaoExists(sessao.SessaoId))
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
            ViewData["FilmeId"] = new SelectList(_context.Filmes, "FilmeId", "Nome");
            ViewData["SalaId"] = new SelectList(_context.Salas, "SalaId", "Numero");
            return View(sessao);
        }

        public bool SessaoExists(long? id)
        {
            return _context.Sessaos.Any(x => x.SessaoId == id);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sessao = await _context.Sessaos.Include(a => a.Filme).Include(a => a.Sala).SingleOrDefaultAsync(a => a.SessaoId == id);
            if (sessao == null)
            {
                NotFound();
            }
            return View(sessao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var sessao = await _context.Sessaos.Include(a => a.Filme).Include(a => a.Sala).SingleOrDefaultAsync(a => a.SessaoId == id);
            _context.Sessaos.Remove(sessao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
