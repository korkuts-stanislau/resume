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
    public class BasketItemsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ClothesShopContext _context;
        private readonly BasketItemService _service;
        private readonly int _pageSize;

        public BasketItemsController(ClothesShopContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _service = new BasketItemService();
            _pageSize = 8;
        }

        // GET: BasketItems
        public async Task<IActionResult> Index(string selectedName, int? page, BasketItemService.SortState? sortState)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User))
            {
                if(!User.IsInRole(Areas.Identity.Roles.Admin))
                {
                    return Redirect("~/Identity/Account/Login");
                }
                return RedirectToAction("Index", "Home");
            }
            bool isFromFilter = HttpContext.Request.Query["isFromFilter"] == "true";

            _service.GetSortPagingCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                ref page, ref sortState);
            _service.GetFilterCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                ref selectedName);
            _service.SetDefaultValuesIfNull(ref selectedName, ref page, ref sortState);
            _service.SetCookies(Response.Cookies, User.Identity.Name, selectedName, page, sortState);

            var basketItems = _context.BasketItems
                .Include(b => b.ClothingItem)
                .AsQueryable();

            if (User.IsInRole(Areas.Identity.Roles.User))
            {
                basketItems = basketItems
                    .Where(b => b.UserId.Equals(_userManager.GetUserId(User)));
            }

            basketItems = _service.Filter(basketItems, selectedName);

            var count = await basketItems.CountAsync();

            basketItems = _service.Sort(basketItems, (BasketItemService.SortState)sortState);
            basketItems = _service.Paging(basketItems, isFromFilter, (int)page, _pageSize);

            ViewModels.BasketItem.IndexBasketItemViewModel model = new ViewModels.BasketItem.IndexBasketItemViewModel
            {
                BasketItems = await basketItems.ToListAsync(),
                PageViewModel = new ViewModels.PageViewModel(count, (int)page, _pageSize),
                FilterBasketItemViewModel = new ViewModels.BasketItem.FilterBasketItemViewModel(selectedName),
                SortBasketItemViewModel = new ViewModels.BasketItem.SortBasketItemViewModel((BasketItemService.SortState)sortState),
            };

            return View(model);
        }

        // GET: BasketItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User))
            {
                if (!User.IsInRole(Areas.Identity.Roles.Admin))
                {
                    return Redirect("~/Identity/Account/Login");
                }
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var basketItem = await _context.BasketItems
                .Include(b => b.ClothingItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basketItem == null)
            {
                return NotFound();
            }

            return View(basketItem);
        }

        // GET: BasketItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User))
            {
                if (!User.IsInRole(Areas.Identity.Roles.Admin))
                {
                    return Redirect("~/Identity/Account/Login");
                }
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var basketItem = await _context.BasketItems
                .Include(b => b.ClothingItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basketItem == null)
            {
                return NotFound();
            }

            return View(basketItem);
        }

        // POST: BasketItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User))
            {
                if (!User.IsInRole(Areas.Identity.Roles.Admin))
                {
                    return Redirect("~/Identity/Account/Login");
                }
                return RedirectToAction("Index", "Home");
            }
            var basketItem = await _context.BasketItems.FindAsync(id);
            _context.BasketItems.Remove(basketItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasketItemExists(int id)
        {
            return _context.BasketItems.Any(e => e.Id == id);
        }
    }
}
