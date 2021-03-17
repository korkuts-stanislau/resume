using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.EntityServices
{
    public class BasketItemService
    {
        public enum SortState
        {
            NameAsc,
            NameDesc
        }
        public IQueryable<Models.BasketItem> Filter(IQueryable<Models.BasketItem> basketItems, string selectedClothingItemName)
        {
            if (!string.IsNullOrEmpty(selectedClothingItemName))
            {
                basketItems = basketItems.Where(p => p.ClothingItem.Name.Contains(selectedClothingItemName));
            }
            return basketItems;
        }

        public IQueryable<Models.BasketItem> Sort(IQueryable<Models.BasketItem> basketItems, SortState sortState)
        {
            switch (sortState)
            {
                case SortState.NameAsc:
                    basketItems = basketItems.OrderBy(p => p.ClothingItem.Name);
                    break;
                case SortState.NameDesc:
                    basketItems = basketItems.OrderByDescending(p => p.ClothingItem.Name);
                    break;
            }
            return basketItems;
        }

        public IQueryable<Models.BasketItem> Paging(IQueryable<Models.BasketItem> basketItems, bool isFromFilter, int page, int pageSize)
        {
            if (isFromFilter)
            {
                page = 1;
            }
            return basketItems.Skip(((int)page - 1) * pageSize).Take(pageSize);
        }

        public void GetFilterCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, ref string selectedName)
        {
            if (string.IsNullOrEmpty(selectedName))
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "basketItemSelectedName", out selectedName);
                }
            }
        }

        public void GetSortPagingCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilter, ref int? page, ref SortState? sortState)
        {
            if (!isFromFilter)
            {
                if (page == null)
                {
                    if (cookies.TryGetValue(username + "basketItemPage", out string pageStr))
                    {
                        page = int.Parse(pageStr);
                    }
                }
                if (sortState == null)
                {
                    if (cookies.TryGetValue(username + "basketItemSortState", out string sortStateStr))
                    {
                        sortState = (SortState)Enum.Parse(typeof(SortState), sortStateStr);
                    }
                }
            }
        }

        public void SetDefaultValuesIfNull(ref string selectedName, ref int? page, ref SortState? sortState)
        {
            selectedName ??= "";
            page ??= 1;
            sortState ??= SortState.NameAsc;
        }

        public void SetCookies(IResponseCookies cookies, string username, string selectedName, int? page, SortState? sortState)
        {
            cookies.Append(username + "basketItemSelectedName", selectedName);
            cookies.Append(username + "basketItemPage", page.ToString());
            cookies.Append(username + "basketItemSortState", sortState.ToString());
        }
    }
}
