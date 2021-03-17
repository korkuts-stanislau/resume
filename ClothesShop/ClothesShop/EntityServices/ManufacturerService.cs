using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.EntityServices
{
    public class ManufacturerService
    {
        public enum SortState
        {
            NameAsc,
            NameDesc
        }
        public IQueryable<Models.Manufacturer> Filter(IQueryable<Models.Manufacturer> manufacturers, string selectedManufacturerName)
        {
            if (!string.IsNullOrEmpty(selectedManufacturerName))
            {
                manufacturers = manufacturers.Where(p => p.Name.Contains(selectedManufacturerName));
            }
            return manufacturers;
        }

        public IQueryable<Models.Manufacturer> Sort(IQueryable<Models.Manufacturer> manufacturers, SortState sortState)
        {
            switch (sortState)
            {
                case SortState.NameAsc:
                    manufacturers = manufacturers.OrderBy(p => p.Name);
                    break;
                case SortState.NameDesc:
                    manufacturers = manufacturers.OrderByDescending(p => p.Name);
                    break;
            }
            return manufacturers;
        }

        public IQueryable<Models.Manufacturer> Paging(IQueryable<Models.Manufacturer> manufacturers, bool isFromFilter, int page, int pageSize)
        {
            if (isFromFilter)
            {
                page = 1;
            }
            return manufacturers.Skip(((int)page - 1) * pageSize).Take(pageSize);
        }

        public void GetFilterCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, ref string selectedName)
        {
            if (string.IsNullOrEmpty(selectedName))
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "manufacturerSelectedName", out selectedName);
                }
            }
        }

        public void GetSortPagingCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilter, ref int? page, ref SortState? sortState)
        {
            if (!isFromFilter)
            {
                if (page == null)
                {
                    if (cookies.TryGetValue(username + "manufacturerPage", out string pageStr))
                    {
                        page = int.Parse(pageStr);
                    }
                }
                if (sortState == null)
                {
                    if (cookies.TryGetValue(username + "manufacturerSortState", out string sortStateStr))
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
            cookies.Append(username + "manufacturerSelectedName", selectedName);
            cookies.Append(username + "manufacturerPage", page.ToString());
            cookies.Append(username + "manufacturerSortState", sortState.ToString());
        }
    }
}
