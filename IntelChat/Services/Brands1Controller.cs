using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntelChat.Models;

namespace IntelChat.Services
{
    public class Brands1Controller : Controller
    {
        private readonly AppDbContext _context;

        public Brands1Controller(AppDbContext context)
        {
            _context = context;
        }

        // GET: Brands1
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Brands.Include(b => b.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Brands1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .Include(b => b.Person)
                .FirstOrDefaultAsync(m => m.Brandid == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Brands1/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId");
            return View();
        }

        // POST: Brands1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Brandid,BrandLink,BrandName,BrandImage,ChannelAlpha,ChannelBeta,ChannelGamma,Status,CntMax,CntReg,Eligibility,RegDateClosed,Cost,ActiveGuideId,ProgramId,LocationId,MenuLock,ScopeLock,PersonId")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId", brand.PersonId);
            return View(brand);
        }

        // GET: Brands1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId", brand.PersonId);
            return View(brand);
        }

        // POST: Brands1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Brandid,BrandLink,BrandName,BrandImage,ChannelAlpha,ChannelBeta,ChannelGamma,Status,CntMax,CntReg,Eligibility,RegDateClosed,Cost,ActiveGuideId,ProgramId,LocationId,MenuLock,ScopeLock,PersonId")] Brand brand)
        {
            if (id != brand.Brandid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Brandid))
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
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId", brand.PersonId);
            return View(brand);
        }

        // GET: Brands1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Brands == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .Include(b => b.Person)
                .FirstOrDefaultAsync(m => m.Brandid == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brands1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Brands == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Brands'  is null.");
            }
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
          return _context.Brands.Any(e => e.Brandid == id);
        }
    }
}
