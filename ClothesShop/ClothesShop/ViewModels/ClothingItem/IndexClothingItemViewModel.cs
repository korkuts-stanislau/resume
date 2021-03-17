using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.ViewModels.ClothingItem
{
    public class IndexClothingItemViewModel
    {
        public IEnumerable<Models.ClothingItem> ClothingItems { get; set; }
        public FilterClothingItemViewModel FilterClothingItemViewModel { get; set; }
        public SortClothingItemViewModel SortClothingItemViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
