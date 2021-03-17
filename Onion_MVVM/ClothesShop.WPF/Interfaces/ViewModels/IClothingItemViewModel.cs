using ClothesShop.BLL.DTO;
using ClothesShop.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.WPF.Interfaces.ViewModels
{
    public interface IClothingItemViewModel : INotifyPropertyChanged
    {
        ClothingItemDTO SelectedClothingItem { get; set; }
        ClothingItemDTO BufferClothingItem { get; set; }
        ObservableCollection<ClothingItemDTO> ClothingItems { get; }
        ObservableCollection<ManufacturerDTO> Manufacturers { get; }
        ObservableCollection<ClothingItemTypeDTO> ClothingItemTypes { get; }
        ManufacturerDTO SelectedManufacturer { get; set; }
        ClothingItemTypeDTO SelectedClothingItemType { get; set; }
        public RelayCommand Add { get; }
        public RelayCommand Edit { get; }
        public RelayCommand Remove { get; }
    }
}
