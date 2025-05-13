using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GreenGrid.Data;
using GreenGrid.Models;

namespace GreenGrid.Controllers
{
    public class EnergyProvidersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnergyProvidersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EnergyProviders
        public async Task<IActionResult> Index()
        {
            return View(await _context.EnergyProviders.ToListAsync());
        }

        // GET: EnergyProviders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var energyProvider = await _context.EnergyProviders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (energyProvider == null)
            {
                return NotFound();
            }

            return View(energyProvider);
        }

        // GET: EnergyProviders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EnergyProviders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyName,ServiceType,Province,ContactEmail,Description,Website")] EnergyProvider energyProvider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(energyProvider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(energyProvider);
        }

        // GET: EnergyProviders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var energyProvider = await _context.EnergyProviders.FindAsync(id);
            if (energyProvider == null)
            {
                return NotFound();
            }
            return View(energyProvider);
        }

        // POST: EnergyProviders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,ServiceType,Province,ContactEmail,Description,Website")] EnergyProvider energyProvider)
        {
            if (id != energyProvider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(energyProvider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnergyProviderExists(energyProvider.Id))
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
            return View(energyProvider);
        }

        // GET: EnergyProviders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var energyProvider = await _context.EnergyProviders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (energyProvider == null)
            {
                return NotFound();
            }

            return View(energyProvider);
        }

        // POST: EnergyProviders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var energyProvider = await _context.EnergyProviders.FindAsync(id);
            if (energyProvider != null)
            {
                _context.EnergyProviders.Remove(energyProvider);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnergyProviderExists(int id)
        {
            return _context.EnergyProviders.Any(e => e.Id == id);
        }
    }
}
