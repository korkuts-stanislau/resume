using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.ViewModels.ClothingItemType
{
    public class FilterClothingItemTypeViewModel
    {
        public string SelectedName { get; set; }

        public FilterClothingItemTypeViewModel(string selectedName)
        {
            SelectedName = selectedName;
        }
    }
}
