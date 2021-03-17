using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.ViewModels.ClothingItem
{
    public class SortClothingItemViewModel
    {
        public EntityServices.ClothingItemService.SortState NameSort { get; set; }
        public EntityServices.ClothingItemService.SortState Current { get; set; }
        public SortClothingItemViewModel(ClothesShop.EntityServices.ClothingItemService.SortState sortState)
        {
            NameSort = sortState == EntityServices.ClothingItemService.SortState.NameAsc ? EntityServices.ClothingItemService.SortState.NameDesc : EntityServices.ClothingItemService.SortState.NameAsc;
            Current = sortState;
        }
    }
}
