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
    public class ContatosEmergenciasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContatosEmergenciasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContatosEmergencias
        public async Task<IActionResult> Index()
        {
            // O erro ocorre porque o nome da propriedade DbSet no ApplicationDbContext está definido como "ContatosEmergencias" (no plural),  
            // mas no código você está tentando acessar "ContatosEmergencia" (no singular).  
            // Para corrigir, altere a linha com o erro para usar o nome correto da propriedade DbSet.  

            var applicationDbContext = _context.ContatosEmergencias.Include(c => c.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ContatosEmergencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatoEmergencia = await _context.ContatosEmergencias
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contatoEmergencia == null)
            {
                return NotFound();
            }

            return View(contatoEmergencia);
        }

        // GET: ContatosEmergencias/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: ContatosEmergencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Telefone,Relacao,UsuarioId")] ContatoEmergencia contatoEmergencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contatoEmergencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", contatoEmergencia.UsuarioId);
            return View(contatoEmergencia);
        }

        // GET: ContatosEmergencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatoEmergencia = await _context.ContatosEmergencias.FindAsync(id);
            if (contatoEmergencia == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", contatoEmergencia.UsuarioId);
            return View(contatoEmergencia);
        }

        // POST: ContatosEmergencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Telefone,Relacao,UsuarioId")] ContatoEmergencia contatoEmergencia)
        {
            if (id != contatoEmergencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contatoEmergencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContatoEmergenciaExists(contatoEmergencia.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", contatoEmergencia.UsuarioId);
            return View(contatoEmergencia);
        }

        // GET: ContatosEmergencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contatoEmergencia = await _context.ContatosEmergencias
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contatoEmergencia == null)
            {
                return NotFound();
            }

            return View(contatoEmergencia);
        }

        // POST: ContatosEmergencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contatoEmergencia = await _context.ContatosEmergencias.FindAsync(id);
            if (contatoEmergencia != null)
            {
                _context.ContatosEmergencias.Remove(contatoEmergencia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContatoEmergenciaExists(int id)
        {
            return _context.ContatosEmergencias.Any(e => e.Id == id);
        }
    }
}
