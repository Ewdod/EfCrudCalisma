using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EfCrudCalisma.Data;

namespace EfCrudCalisma.Controllers
{
    public class SozlukController : Controller
    {
        private readonly SozlukContext _context;

        public SozlukController(SozlukContext context)
        {
            _context = context;
        }

        // GET: Sozluk
        public async Task<IActionResult> Index()
        {
              return _context.Girdiler != null ? 
                          View(await _context.Girdiler.ToListAsync()) :
                          Problem("Entity set 'SozlukContext.Girdiler'  is null.");
        }

        // GET: Sozluk/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Girdiler == null)
            {
                return NotFound();
            }

            var girdi = await _context.Girdiler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (girdi == null)
            {
                return NotFound();
            }

            return View(girdi);
        }

        // GET: Sozluk/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sozluk/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Sozcuk,Anlam")] Girdi girdi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(girdi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(girdi);
        }

        // GET: Sozluk/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Girdiler == null)
            {
                return NotFound();
            }

            var girdi = await _context.Girdiler.FindAsync(id);
            if (girdi == null)
            {
                return NotFound();
            }
            return View(girdi);
        }

        // POST: Sozluk/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sozcuk,Anlam")] Girdi girdi)
        {
            if (id != girdi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(girdi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GirdiExists(girdi.Id))
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
            return View(girdi);
        }

        // GET: Sozluk/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Girdiler == null)
            {
                return NotFound();
            }

            var girdi = await _context.Girdiler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (girdi == null)
            {
                return NotFound();
            }

            return View(girdi);
        }

        // POST: Sozluk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Girdiler == null)
            {
                return Problem("Entity set 'SozlukContext.Girdiler'  is null.");
            }
            var girdi = await _context.Girdiler.FindAsync(id);
            if (girdi != null)
            {
                _context.Girdiler.Remove(girdi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GirdiExists(int id)
        {
          return (_context.Girdiler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
