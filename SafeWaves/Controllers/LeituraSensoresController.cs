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
    public class LeituraSensoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeituraSensoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeituraSensores
        public async Task<IActionResult> Index()
        {
            var leituraSensores = _context.LeituraSensores.Include(l => l.Sensor);
            return View(await leituraSensores.ToListAsync());
        }

        // GET: LeituraSensores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var leituraSensor = await _context.LeituraSensores
                .Include(l => l.Sensor)
                .FirstOrDefaultAsync(m => m.LeituraSensorId == id);

            if (leituraSensor == null)
                return NotFound();

            return View(leituraSensor);
        }

        // GET: LeituraSensores/Create
        public IActionResult Create()
        {
            ViewData["SensorId"] = new SelectList(_context.Sensores, "Id", "Id");
            return View();
        }

        // POST: LeituraSensores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SensorId,DataHora,Valor")] LeituraSensor leituraSensor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leituraSensor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SensorId"] = new SelectList(_context.Sensores, "Id", "Id", leituraSensor.SensorId);
            return View(leituraSensor);
        }

        // GET: LeituraSensores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var leituraSensor = await _context.LeituraSensores.FindAsync(id);
            if (leituraSensor == null)
                return NotFound();

            ViewData["SensorId"] = new SelectList(_context.Sensores, "Id", "Id", leituraSensor.SensorId);
            return View(leituraSensor);
        }

        // POST: LeituraSensores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SensorId,DataHora,Valor")] LeituraSensor leituraSensor)
        {
            if (id != leituraSensor.LeituraSensorId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leituraSensor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeituraSensorExists(leituraSensor.LeituraSensorId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SensorId"] = new SelectList(_context.Sensores, "Id", "Id", leituraSensor.SensorId);
            return View(leituraSensor);
        }

        // GET: LeituraSensores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var leituraSensor = await _context.LeituraSensores
                .Include(l => l.Sensor)
                .FirstOrDefaultAsync(m => m.LeituraSensorId == id);

            if (leituraSensor == null)
                return NotFound();

            return View(leituraSensor);
        }

        // POST: LeituraSensores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leituraSensor = await _context.LeituraSensores.FindAsync(id);
            if (leituraSensor != null)
            {
                _context.LeituraSensores.Remove(leituraSensor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool LeituraSensorExists(int id)
        {
            return _context.LeituraSensores.Any(e => e.LeituraSensorId == id);
        }
    }
}
