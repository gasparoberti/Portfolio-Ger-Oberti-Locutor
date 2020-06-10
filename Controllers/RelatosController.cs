using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortfolioCore.Data;
using PortfolioCore.Models;

namespace PortfolioCore.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class RelatosController : Controller
    {
        private readonly MvcRelatoContext _context;

        public RelatosController(MvcRelatoContext context)
        {
            _context = context;
        }

        // GET: Relatos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Relato.ToListAsync());
        }

        // GET: Relatos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relato = await _context.Relato
                .FirstOrDefaultAsync(m => m.id == id);
            if (relato == null)
            {
                return NotFound();
            }

            return View(relato);
        }

        // GET: Relatos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Relatos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,url,visible")] Relato relato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(relato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(relato);
        }

        // GET: Relatos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relato = await _context.Relato.FindAsync(id);
            if (relato == null)
            {
                return NotFound();
            }
            return View(relato);
        }

        // POST: Relatos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,url,visible")] Relato relato)
        {
            if (id != relato.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelatoExists(relato.id))
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
            return View(relato);
        }

        // GET: Relatos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relato = await _context.Relato
                .FirstOrDefaultAsync(m => m.id == id);
            if (relato == null)
            {
                return NotFound();
            }

            return View(relato);
        }

        // POST: Relatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var relato = await _context.Relato.FindAsync(id);
            _context.Relato.Remove(relato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelatoExists(int id)
        {
            return _context.Relato.Any(e => e.id == id);
        }
    }
}
