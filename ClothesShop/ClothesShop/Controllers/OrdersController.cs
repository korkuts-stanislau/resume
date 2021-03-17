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
using ClothesShop.ViewModels.Order;

namespace ClothesShop.Controllers
{
    public class OrdersController : Controller
    {
        UserManager<IdentityUser> _userManager;
        private readonly ClothesShopContext _context;
        private readonly OrderService _service;
        private readonly int _pageSize;

        public OrdersController(ClothesShopContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _service = new OrderService();
            _pageSize = 8;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string selectedName, int? page, OrderService.SortState? sortState)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User) && !User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            bool isFromFilter = HttpContext.Request.Query["isFromFilter"] == "true";

            _service.GetSortPagingCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                ref page, ref sortState);
            _service.GetFilterCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter);
            _service.SetDefaultValuesIfNull(ref page, ref sortState);
            _service.SetCookies(Response.Cookies, User.Identity.Name, page, sortState);

            var orders = _context.Orders
                .AsQueryable();

            if (User.IsInRole(Areas.Identity.Roles.User))
            {
                orders = orders
                .Where(o => o.UserId.Equals(_userManager.GetUserId(User)));
            }

            orders = _service.Filter(orders);

            var count = await orders.CountAsync();

            orders = _service.Sort(orders, (OrderService.SortState)sortState);
            orders = _service.Paging(orders, isFromFilter, (int)page, _pageSize);

            ViewModels.Order.IndexOrderViewModel model = new ViewModels.Order.IndexOrderViewModel
            {
                Orders = await orders.ToListAsync(),
                PageViewModel = new ViewModels.PageViewModel(count, (int)page, _pageSize)
            };

            return View(model);
        }

        // GET: Orders/Details/5
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

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            var orderItems = _context.OrderClothingItems
                .Where(o => o.OrderId == order.Id)
                .Include(o => o.ClothingItem)
                .Include(o => o.Order);

            var model = new DetailsOrderViewModel
            {
                Order = order,
                OrderClothingItems = orderItems.ToList()
            };

            return View(model);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerName,CustomerPhone,CustomerAddress,Id")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.UserId = _userManager.GetUserId(User);
                order.Date = DateTime.Now;
                order.IsPaid = false;
                order.IsSent = false;
                _context.Add(order);
                _context.SaveChanges();

                int orderToAddId = _context.Orders.Select(o => o.Id).AsEnumerable().LastOrDefault();

                var basketItems = _context.BasketItems
                    .Where(b => b.UserId == _userManager.GetUserId(User));
                var orderItems = basketItems
                    .Select(b => new OrderClothingItem
                    {
                        OrderId = orderToAddId,
                        ClothingItemId = b.ClothingItemId,
                        Count = b.Count
                    });

                _context.BasketItems.RemoveRange(basketItems);
                _context.OrderClothingItems.AddRange(orderItems);

                _context.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = orderToAddId });
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Orders");
            }
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerName,CustomerPhone,CustomerAddress,Date,IsPaid,IsSent,Id")] Order order)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Orders");
            }
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.UserId = _userManager.GetUserId(User);
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Orders");
            }
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Orders");
            }
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PayForOrder(int id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User))
            {
                return RedirectToAction("Index", "Orders");
            }
            var order = await _context.Orders.FindAsync(id);
            order.IsPaid = true;
            _context.Orders.Update(order);
            _context.SaveChanges();
            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendOrder(int id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Orders");
            }
            var order = await _context.Orders.FindAsync(id);
            order.IsSent = true;
            _context.Orders.Update(order);
            _context.SaveChanges();
            return RedirectToAction(nameof(Details), new { id = id });
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
