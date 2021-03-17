using ClothesShop.WPF.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.WPF.ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        public MainViewModel(IManufacturerViewModel manufacturerViewModel, IClothingItemTypeViewModel clothingItemTypeViewModel, 
            IClothingItemViewModel clothingItemViewModel)
        {
            ManufacturerViewModel = manufacturerViewModel;
            ClothingItemTypeViewModel = clothingItemTypeViewModel;
            ClothingItemViewModel = clothingItemViewModel;
        }
        public IManufacturerViewModel ManufacturerViewModel { get; set; }
        public IClothingItemTypeViewModel ClothingItemTypeViewModel { get; set; }
        public IClothingItemViewModel ClothingItemViewModel { get; set; }
    }
}
