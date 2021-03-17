using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.ViewModels.ClothingItemType
{
    public class SortClothingItemTypeViewModel
    {
        public EntityServices.ClothingItemTypeService.SortState NameSort { get; set; }
        public EntityServices.ClothingItemTypeService.SortState Current { get; set; }
        public SortClothingItemTypeViewModel(ClothesShop.EntityServices.ClothingItemTypeService.SortState sortState)
        {
            NameSort = sortState == EntityServices.ClothingItemTypeService.SortState.NameAsc ? EntityServices.ClothingItemTypeService.SortState.NameDesc : EntityServices.ClothingItemTypeService.SortState.NameAsc;
            Current = sortState;
        }
    }
}
