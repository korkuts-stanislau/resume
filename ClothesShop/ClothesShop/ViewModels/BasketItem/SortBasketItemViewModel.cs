using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.ViewModels.BasketItem
{
    public class SortBasketItemViewModel
    {
        public EntityServices.BasketItemService.SortState NameSort { get; set; }
        public EntityServices.BasketItemService.SortState Current { get; set; }
        public SortBasketItemViewModel(EntityServices.BasketItemService.SortState sortState)
        {
            NameSort = sortState == EntityServices.BasketItemService.SortState.NameAsc ? EntityServices.BasketItemService.SortState.NameDesc : EntityServices.BasketItemService.SortState.NameAsc;
            Current = sortState;
        }
    }
}
