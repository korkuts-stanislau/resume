using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClothesShop.Data;
using ClothesShop.Models;

namespace ClothesShop.Controllers
{
    public class OrderClothingItemsController : Controller
    {
        private readonly ClothesShopContext _context;

        public OrderClothingItemsController(ClothesShopContext context)
        {
            _context = context;
        }

        // GET: OrderClothingItems
        public async Task<IActionResult> Index()
        {
            var clothesShopContext = _context.OrderClothingItems.Include(o => o.ClothingItem).Include(o => o.Order);
            return View(await clothesShopContext.ToListAsync());
        }

        // GET: OrderClothingItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderClothingItem = await _context.OrderClothingItems
                .Include(o => o.ClothingItem)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderClothingItem == null)
            {
                return NotFound();
            }

            return View(orderClothingItem);
        }

        // GET: OrderClothingItems/Create
        public IActionResult Create()
        {
            ViewData["ClothingItemId"] = new SelectList(_context.ClothingItems, "Id", "Description");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "CustomerAddress");
            return View();
        }

        // POST: OrderClothingItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ClothingItemId,Count,Id")] OrderClothingItem orderClothingItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderClothingItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClothingItemId"] = new SelectList(_context.ClothingItems, "Id", "Description", orderClothingItem.ClothingItemId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "CustomerAddress", orderClothingItem.OrderId);
            return View(orderClothingItem);
        }

        // GET: OrderClothingItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderClothingItem = await _context.OrderClothingItems.FindAsync(id);
            if (orderClothingItem == null)
            {
                return NotFound();
            }
            ViewData["ClothingItemId"] = new SelectList(_context.ClothingItems, "Id", "Description", orderClothingItem.ClothingItemId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "CustomerAddress", orderClothingItem.OrderId);
            return View(orderClothingItem);
        }

        // POST: OrderClothingItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ClothingItemId,Count,Id")] OrderClothingItem orderClothingItem)
        {
            if (id != orderClothingItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderClothingItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderClothingItemExists(orderClothingItem.Id))
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
            ViewData["ClothingItemId"] = new SelectList(_context.ClothingItems, "Id", "Description", orderClothingItem.ClothingItemId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "CustomerAddress", orderClothingItem.OrderId);
            return View(orderClothingItem);
        }

        // GET: OrderClothingItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderClothingItem = await _context.OrderClothingItems
                .Include(o => o.ClothingItem)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderClothingItem == null)
            {
                return NotFound();
            }

            return View(orderClothingItem);
        }

        // POST: OrderClothingItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderClothingItem = await _context.OrderClothingItems.FindAsync(id);
            _context.OrderClothingItems.Remove(orderClothingItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderClothingItemExists(int id)
        {
            return _context.OrderClothingItems.Any(e => e.Id == id);
        }
    }
}
