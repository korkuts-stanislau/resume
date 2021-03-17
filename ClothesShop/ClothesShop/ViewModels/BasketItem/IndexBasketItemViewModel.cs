using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.ViewModels.BasketItem
{
    public class IndexBasketItemViewModel
    {
        public IEnumerable<Models.BasketItem> BasketItems { get; set; }
        public FilterBasketItemViewModel FilterBasketItemViewModel { get; set; }
        public SortBasketItemViewModel SortBasketItemViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
