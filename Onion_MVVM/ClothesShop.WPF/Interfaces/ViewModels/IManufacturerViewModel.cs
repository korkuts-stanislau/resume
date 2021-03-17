using ClothesShop.BLL.DTO;
using ClothesShop.BLL.Interfaces.EntityServices;
using ClothesShop.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.WPF.Interfaces.ViewModels
{
    public interface IManufacturerViewModel : INotifyPropertyChanged
    {
        ManufacturerDTO SelectedManufacturer { get; set; }
        ManufacturerDTO BufferManufacturer { get; set; }
        ObservableCollection<ManufacturerDTO> Manufacturers { get; }
        public RelayCommand Add { get; }
        public RelayCommand Edit { get; }
        public RelayCommand Remove { get; }
    }
}
