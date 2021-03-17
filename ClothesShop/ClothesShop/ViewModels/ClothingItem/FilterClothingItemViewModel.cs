using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.ViewModels.ClothingItem
{
    public class FilterClothingItemViewModel
    {
        public string SelectedName { get; set; }

        public FilterClothingItemViewModel(string selectedName)
        {
            SelectedName = selectedName;
        }
    }
}
