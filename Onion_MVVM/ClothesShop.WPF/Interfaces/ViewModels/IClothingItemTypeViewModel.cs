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
    public interface IClothingItemTypeViewModel : INotifyPropertyChanged
    {
        ClothingItemTypeDTO SelectedClothingItemType { get; set; }
        ClothingItemTypeDTO BufferClothingItemType { get; set; }
        ObservableCollection<ClothingItemTypeDTO> ClothingItemTypes { get; }
        public RelayCommand Add { get; }
        public RelayCommand Edit { get; }
        public RelayCommand Remove { get; }
    }
}
