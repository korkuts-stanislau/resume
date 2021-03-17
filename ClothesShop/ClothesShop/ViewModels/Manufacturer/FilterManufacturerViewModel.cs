using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.ViewModels.Manufacturer
{
    public class FilterManufacturerViewModel
    {
        public string SelectedName { get; set; }

        public FilterManufacturerViewModel(string selectedName)
        {
            SelectedName = selectedName;
        }
    }
}
