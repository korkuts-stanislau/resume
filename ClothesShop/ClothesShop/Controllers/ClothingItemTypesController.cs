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

namespace ClothesShop.Controllers
{
    public class ClothingItemTypesController : Controller
    {
        private readonly ClothesShopContext _context;
        private readonly ClothingItemTypeService _service;
        private readonly int _pageSize;

        public ClothingItemTypesController(ClothesShopContext context)
        {
            _context = context;
            _service = new ClothingItemTypeService();
            _pageSize = 6;
        }

        // GET: ClothingItemTypes
        public async Task<IActionResult> Index(string selectedName, int? page, ClothingItemTypeService.SortState? sortState)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
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

            var clothingItemTypes = _context.ClothingItemTypes.AsQueryable();

            clothingItemTypes = _service.Filter(clothingItemTypes, selectedName);

            var count = await clothingItemTypes.CountAsync();

            clothingItemTypes = _service.Sort(clothingItemTypes, (ClothingItemTypeService.SortState)sortState);
            clothingItemTypes = _service.Paging(clothingItemTypes, isFromFilter, (int)page, _pageSize);

            ViewModels.ClothingItemType.IndexClothingItemTypeViewModel model = new ViewModels.ClothingItemType.IndexClothingItemTypeViewModel
            {
                ClothingItemTypes = await clothingItemTypes.ToListAsync(),
                PageViewModel = new ViewModels.PageViewModel(count, (int)page, _pageSize),
                FilterClothingItemTypeViewModel = new ViewModels.ClothingItemType.FilterClothingItemTypeViewModel(selectedName),
                SortClothingItemTypeViewModel = new ViewModels.ClothingItemType.SortClothingItemTypeViewModel((ClothingItemTypeService.SortState)sortState),
            };

            return View(model);
        }

        // GET: ClothingItemTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var clothingItemType = await _context.ClothingItemTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingItemType == null)
            {
                return NotFound();
            }

            return View(clothingItemType);
        }

        // GET: ClothingItemTypes/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            return View();
        }

        // POST: ClothingItemTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Id")] ClothingItemType clothingItemType)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            if (ModelState.IsValid)
            {
                _context.Add(clothingItemType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clothingItemType);
        }

        // GET: ClothingItemTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var clothingItemType = await _context.ClothingItemTypes.FindAsync(id);
            if (clothingItemType == null)
            {
                return NotFound();
            }
            return View(clothingItemType);
        }

        // POST: ClothingItemTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Id")] ClothingItemType clothingItemType)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            if (id != clothingItemType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clothingItemType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothingItemTypeExists(clothingItemType.Id))
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
            return View(clothingItemType);
        }

        // GET: ClothingItemTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var clothingItemType = await _context.ClothingItemTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clothingItemType == null)
            {
                return NotFound();
            }

            return View(clothingItemType);
        }

        // POST: ClothingItemTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            var clothingItemType = await _context.ClothingItemTypes.FindAsync(id);
            _context.ClothingItemTypes.Remove(clothingItemType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClothingItemTypeExists(int id)
        {
            return _context.ClothingItemTypes.Any(e => e.Id == id);
        }
    }
}
