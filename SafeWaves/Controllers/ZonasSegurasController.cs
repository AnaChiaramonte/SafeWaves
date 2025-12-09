using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SafeWaves.Data;
using SafeWaves.Models;

namespace SafeWaves.Controllers
{
    public class ZonasSegurasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZonasSegurasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ZonasSeguras
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ZonasSeguras.Include(z => z.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ZonasSeguras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zonaSegura = await _context.ZonasSeguras
                .Include(z => z.Usuario)
                .FirstOrDefaultAsync(m => m.ZonaSeguraId == id);
            if (zonaSegura == null)
            {
                return NotFound();
            }

            return View(zonaSegura);
        }

        // GET: ZonasSeguras/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: ZonasSeguras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,EZonaDeRisco,UsuarioId")] ZonaSegura zonaSegura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zonaSegura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", zonaSegura.UsuarioId);
            return View(zonaSegura);
        }

        // GET: ZonasSeguras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zonaSegura = await _context.ZonasSeguras.FindAsync(id);
            if (zonaSegura == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", zonaSegura.UsuarioId);
            return View(zonaSegura);
        }

        // POST: ZonasSeguras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,EZonaDeRisco,UsuarioId")] ZonaSegura zonaSegura)
        {
            if (id != zonaSegura.ZonaSeguraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zonaSegura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZonaSeguraExists(zonaSegura.ZonaSeguraId))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", zonaSegura.UsuarioId);
            return View(zonaSegura);
        }

        // GET: ZonasSeguras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zonaSegura = await _context.ZonasSeguras
                .Include(z => z.Usuario)
                .FirstOrDefaultAsync(m => m.ZonaSeguraId == id);
            if (zonaSegura == null)
            {
                return NotFound();
            }

            return View(zonaSegura);
        }

        // POST: ZonasSeguras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zonaSegura = await _context.ZonasSeguras.FindAsync(id);
            if (zonaSegura != null)
            {
                _context.ZonasSeguras.Remove(zonaSegura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZonaSeguraExists(int id)
        {
            return _context.ZonasSeguras.Any(e => e.ZonaSeguraId == id);
        }
    }
}
