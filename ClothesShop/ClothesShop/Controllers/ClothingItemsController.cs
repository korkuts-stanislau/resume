using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClothesShop.Data;
using ClothesShop.Models;
using ClothesShop.EntityServices;
using Microsoft.AspNetCore.Identity;

namespace ClothesShop.Controllers
{
    public class ClothingItemsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ClothesShopContext _context;
        private readonly ClothingItemService _service;
        private readonly int _pageSize;

        public ClothingItemsController(ClothesShopContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _service = new ClothingItemService();
            _pageSize = 5;
        }

        // GET: ClothingItems
        public async Task<IActionResult> Index(string selectedName, int? page, ClothingItemService.SortState? sortState)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User) && !User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            bool isFromFilter = HttpContext.Request.Query["isFromFilter"] == "true";

            _service.GetSortPagingCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                ref page, ref sortState);
            _service.GetFilterCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                ref selectedName);
            _service.SetDefaultValuesIfNull(ref selectedName, ref page, ref sortState);
            _service.SetCookies(Response.Cookies, User.Identity.Name, selectedName, page, sortState);

            var clothingItems = _context.ClothingItems
                .Include(c => c.Type)
                .Include(c => c.Manufacturer)
                .AsQueryable();

            clothingItems = _service.Filter(clothingItems, selectedName);

            var count = await clothingItems.CountAsync();

            clothingItems = _service.Sort(clothingItems, (ClothingItemService.SortState)sortState);
            clothingItems = _service.Paging(clothingItems, isFromFilter, (int)page, _pageSize);

            ViewModels.ClothingItem.IndexClothingItemViewModel model = new ViewModels.ClothingItem.IndexClothingItemViewModel
            {
                ClothingItems = await clothingItems.ToListAsync(),
                PageViewModel = new ViewModels.PageViewModel(count, (int)page, _pageSize),
                FilterClothingItemViewModel = new ViewModels.ClothingItem.FilterClothingItemViewModel(selectedName),
                SortClothingItemViewModel = new ViewModels.ClothingItem.SortClothingItemViewModel((ClothingItemService.SortState)sortState),
            };

            return View(model);
        }

        // GET: ClothingItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User) && !User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var clothingItem = await _context.ClothingItems
                .Include(c => c.Manufacturer)
                .Include(c => c.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingItem == null)
            {
                return NotFound();
            }

            return View(clothingItem);
        }

        // GET: ClothingItems/Create
        public IActionResult Create()
        {
            if(!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            ViewData["TypeId"] = new SelectList(_context.ClothingItemTypes, "Id", "Name");
            return View();
        }

        // POST: ClothingItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeId,ManufacturerId,Name,Description,Size,IsMale,Price,Id")] ClothingItem clothingItem)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(clothingItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", clothingItem.ManufacturerId);
            ViewData["TypeId"] = new SelectList(_context.ClothingItemTypes, "Id", "Name", clothingItem.TypeId);
            return View(clothingItem);
        }

        // GET: ClothingItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var clothingItem = await _context.ClothingItems.FindAsync(id);
            if (clothingItem == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", clothingItem.ManufacturerId);
            ViewData["TypeId"] = new SelectList(_context.ClothingItemTypes, "Id", "Name", clothingItem.TypeId);
            return View(clothingItem);
        }

        // POST: ClothingItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeId,ManufacturerId,Name,Description,Size,IsMale,Price,Id")] ClothingItem clothingItem)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != clothingItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clothingItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothingItemExists(clothingItem.Id))
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
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", clothingItem.ManufacturerId);
            ViewData["TypeId"] = new SelectList(_context.ClothingItemTypes, "Id", "Name", clothingItem.TypeId);
            return View(clothingItem);
        }

        // GET: ClothingItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var clothingItem = await _context.ClothingItems
                .Include(c => c.Manufacturer)
                .Include(c => c.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingItem == null)
            {
                return NotFound();
            }

            return View(clothingItem);
        }

        // POST: ClothingItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Home");
            }
            var clothingItem = await _context.ClothingItems.FindAsync(id);
            _context.ClothingItems.Remove(clothingItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToBasket(int id, int? count)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User))
            {
                return RedirectToAction("Index");
            }
            await _context.BasketItems.AddAsync(new BasketItem
            {
                UserId = _userManager.GetUserId(User),
                ClothingItemId = id,
                Count = count ?? 1
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        private bool ClothingItemExists(int id)
        {
            return _context.ClothingItems.Any(e => e.Id == id);
        }
    }
}
