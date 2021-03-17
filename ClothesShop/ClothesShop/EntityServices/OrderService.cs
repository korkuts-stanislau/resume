using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.EntityServices
{
    public class OrderService
    {
        public enum SortState
        {
           No
        }
        public IQueryable<Models.Order> Filter(IQueryable<Models.Order> orders)
        {
            return orders;
        }

        public IQueryable<Models.Order> Sort(IQueryable<Models.Order> orders, SortState sortState)
        {
            switch (sortState)
            {
                
            }
            return orders;
        }

        public IQueryable<Models.Order> Paging(IQueryable<Models.Order> orders, bool isFromFilter, int page, int pageSize)
        {
            if (isFromFilter)
            {
                page = 1;
            }
            return orders.Skip(((int)page - 1) * pageSize).Take(pageSize);
        }

        public void GetFilterCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm)
        {
        }

        public void GetSortPagingCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilter, ref int? page, ref SortState? sortState)
        {
            if (!isFromFilter)
            {
                if (page == null)
                {
                    if (cookies.TryGetValue(username + "orderPage", out string pageStr))
                    {
                        page = int.Parse(pageStr);
                    }
                }
                if (sortState == null)
                {
                    if (cookies.TryGetValue(username + "orderSortState", out string sortStateStr))
                    {
                        sortState = (SortState)Enum.Parse(typeof(SortState), sortStateStr);
                    }
                }
            }
        }

        public void SetDefaultValuesIfNull(ref int? page, ref SortState? sortState)
        {
            page ??= 1;
            sortState ??= SortState.No;
        }

        public void SetCookies(IResponseCookies cookies, string username, int? page, SortState? sortState)
        {
            cookies.Append(username + "orderPage", page.ToString());
            cookies.Append(username + "orderSortState", sortState.ToString());
        }
    }
}
