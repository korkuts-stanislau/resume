using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.EntityServices
{
    public class ClothingItemTypeService
    {
        public enum SortState
        {
            NameAsc,
            NameDesc
        }
        public IQueryable<Models.ClothingItemType> Filter(IQueryable<Models.ClothingItemType> clothingItemTypes, string selectedClothingItemTypeName)
        {
            if (!string.IsNullOrEmpty(selectedClothingItemTypeName))
            {
                clothingItemTypes = clothingItemTypes.Where(p => p.Name.Contains(selectedClothingItemTypeName));
            }
            return clothingItemTypes;
        }

        public IQueryable<Models.ClothingItemType> Sort(IQueryable<Models.ClothingItemType> clothingItemTypes, SortState sortState)
        {
            switch (sortState)
            {
                case SortState.NameAsc:
                    clothingItemTypes = clothingItemTypes.OrderBy(p => p.Name);
                    break;
                case SortState.NameDesc:
                    clothingItemTypes = clothingItemTypes.OrderByDescending(p => p.Name);
                    break;
            }
            return clothingItemTypes;
        }

        public IQueryable<Models.ClothingItemType> Paging(IQueryable<Models.ClothingItemType> clothingItemTypes, bool isFromFilter, int page, int pageSize)
        {
            if (isFromFilter)
            {
                page = 1;
            }
            return clothingItemTypes.Skip(((int)page - 1) * pageSize).Take(pageSize);
        }

        public void GetFilterCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, ref string selectedName)
        {
            if (string.IsNullOrEmpty(selectedName))
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "clothingItemTypeSelectedName", out selectedName);
                }
            }
        }

        public void GetSortPagingCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilter, ref int? page, ref SortState? sortState)
        {
            if (!isFromFilter)
            {
                if (page == null)
                {
                    if (cookies.TryGetValue(username + "clothingItemTypePage", out string pageStr))
                    {
                        page = int.Parse(pageStr);
                    }
                }
                if (sortState == null)
                {
                    if (cookies.TryGetValue(username + "clothingItemTypeSortState", out string sortStateStr))
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
            cookies.Append(username + "clothingItemTypeSelectedName", selectedName);
            cookies.Append(username + "clothingItemTypePage", page.ToString());
            cookies.Append(username + "clothingItemTypeSortState", sortState.ToString());
        }
    }
}
