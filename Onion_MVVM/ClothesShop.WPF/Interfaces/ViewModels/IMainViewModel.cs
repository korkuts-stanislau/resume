using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.WPF.Interfaces.ViewModels
{
    public interface IMainViewModel
    {
        public IManufacturerViewModel ManufacturerViewModel { get; set; }
        public IClothingItemTypeViewModel ClothingItemTypeViewModel { get; set; }
        public IClothingItemViewModel ClothingItemViewModel { get; set; }
    }
}
