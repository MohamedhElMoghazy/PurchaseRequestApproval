using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PurchaseRequestApproval.DataAccess.Data;
using PurchaseRequestApproval.Models;

namespace PurchaseRequestApproval.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PurchaseTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PurchaseTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PurchaseTypes.ToListAsync());
        }

        // GET: Admin/PurchaseTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseType = await _context.PurchaseTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseType == null)
            {
                return NotFound();
            }

            return View(purchaseType);
        }

        // GET: Admin/PurchaseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PurchaseTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PurcahseTypeName,PurcahseCode")] PurchaseType purchaseType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseType);
        }

        // GET: Admin/PurchaseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseType = await _context.PurchaseTypes.FindAsync(id);
            if (purchaseType == null)
            {
                return NotFound();
            }
            return View(purchaseType);
        }

        // POST: Admin/PurchaseTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PurcahseTypeName,PurcahseCode")] PurchaseType purchaseType)
        {
            if (id != purchaseType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseTypeExists(purchaseType.Id))
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
            return View(purchaseType);
        }

        // GET: Admin/PurchaseTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseType = await _context.PurchaseTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseType == null)
            {
                return NotFound();
            }

            return View(purchaseType);
        }

        // POST: Admin/PurchaseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseType = await _context.PurchaseTypes.FindAsync(id);
            _context.PurchaseTypes.Remove(purchaseType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseTypeExists(int id)
        {
            return _context.PurchaseTypes.Any(e => e.Id == id);
        }
    }
}
