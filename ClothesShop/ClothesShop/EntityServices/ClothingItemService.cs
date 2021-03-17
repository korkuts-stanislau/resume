using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.EntityServices
{
    public class ClothingItemService
    {
        public enum SortState
        {
            NameAsc,
            NameDesc
        }
        public IQueryable<Models.ClothingItem> Filter(IQueryable<Models.ClothingItem> clothingItems, string selectedClothingItemName)
        {
            if (!string.IsNullOrEmpty(selectedClothingItemName))
            {
                clothingItems = clothingItems.Where(p => p.Name.Contains(selectedClothingItemName));
            }
            return clothingItems;
        }

        public IQueryable<Models.ClothingItem> Sort(IQueryable<Models.ClothingItem> clothingItems, SortState sortState)
        {
            switch (sortState)
            {
                case SortState.NameAsc:
                    clothingItems = clothingItems.OrderBy(p => p.Name);
                    break;
                case SortState.NameDesc:
                    clothingItems = clothingItems.OrderByDescending(p => p.Name);
                    break;
            }
            return clothingItems;
        }

        public IQueryable<Models.ClothingItem> Paging(IQueryable<Models.ClothingItem> clothingItems, bool isFromFilter, int page, int pageSize)
        {
            if (isFromFilter)
            {
                page = 1;
            }
            return clothingItems.Skip(((int)page - 1) * pageSize).Take(pageSize);
        }

        public void GetFilterCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, ref string selectedName)
        {
            if (string.IsNullOrEmpty(selectedName))
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "clothingItemSelectedName", out selectedName);
                }
            }
        }

        public void GetSortPagingCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilter, ref int? page, ref SortState? sortState)
        {
            if (!isFromFilter)
            {
                if (page == null)
                {
                    if (cookies.TryGetValue(username + "clothingItemPage", out string pageStr))
                    {
                        page = int.Parse(pageStr);
                    }
                }
                if (sortState == null)
                {
                    if (cookies.TryGetValue(username + "clothingItemSortState", out string sortStateStr))
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
            cookies.Append(username + "clothingItemSelectedName", selectedName);
            cookies.Append(username + "clothingItemPage", page.ToString());
            cookies.Append(username + "clothingItemSortState", sortState.ToString());
        }
    }
}
