using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.ViewModels.BasketItem
{
    public class FilterBasketItemViewModel
    {
        public string SelectedName { get; set; }

        public FilterBasketItemViewModel(string selectedName)
        {
            SelectedName = selectedName;
        }
    }
}
