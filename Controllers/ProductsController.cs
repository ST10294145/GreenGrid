using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GreenGrid.Data;
using GreenGrid.Models;

namespace GreenGrid.Controllers
{
    [Authorize] // Ensure user is authenticated before accessing any action
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        [Authorize(Roles = "Employee, Admin")] // Allow employees and admin to view products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Farmer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        [Authorize(Roles = "Employee, Admin")] // Allow employees and admin to view details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Farmer)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Farmer, Admin")] // Only farmers and admin can create products
        public IActionResult Create()
        {
            ViewData["FarmerId"] = new SelectList(_context.Farmers, "FarmerId", "Location");
            return View();
        }

        // POST: Products/Create
        [Authorize(Roles = "Farmer, Admin")] // Only farmers and admin can create products
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Category,ProductionDate,FarmerId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FarmerId"] = new SelectList(_context.Farmers, "FarmerId", "Location", product.FarmerId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Farmer, Admin")] // Only farmers and admin can edit products
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["FarmerId"] = new SelectList(_context.Farmers, "FarmerId", "Location", product.FarmerId);
            return View(product);
        }

        // POST: Products/Edit/5
        [Authorize(Roles = "Farmer, Admin")] // Only farmers and admin can edit products
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Category,ProductionDate,FarmerId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["FarmerId"] = new SelectList(_context.Farmers, "FarmerId", "Location", product.FarmerId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Farmer, Admin")] // Only farmers and admin can delete products
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Farmer)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [Authorize(Roles = "Farmer, Admin")] // Only farmers and admin can delete products
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
