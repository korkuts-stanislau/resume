using ClothesShop.BLL.DTO;
using ClothesShop.BLL.Interfaces.EntityServices;
using ClothesShop.BLL.Services;
using ClothesShop.WPF.Commands;
using ClothesShop.WPF.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClothesShop.WPF.ViewModels
{
    public class ClothingItemViewModel : IClothingItemViewModel
    {
        private IManufacturerService _manufacturerService;
        private IClothingItemTypeService _clothingItemTypeService;
        private IClothingItemService _clothingItemService;

        public ClothingItemViewModel(IManufacturerService mans, IClothingItemTypeService types, IClothingItemService items)
        {
            _manufacturerService = mans;
            _clothingItemService = items;
            _clothingItemTypeService = types;
            _bufItem = new ClothingItemDTO();
        }

        

        private ClothingItemDTO _selectedItem;
        public ClothingItemDTO SelectedClothingItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedClothingItem");
            }
        }
        private ClothingItemDTO _bufItem;
        public ClothingItemDTO BufferClothingItem
        {
            get
            {
                return _bufItem;
            }
            set
            {
                _bufItem = value;
                OnPropertyChanged("BufferClothingItem");
            }
        }
        private ObservableCollection<ClothingItemDTO> _items;
        public ObservableCollection<ClothingItemDTO> ClothingItems
        {
            get
            {
                _items = GetItems();
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged("ClothingItems");
            }
        }

        private ObservableCollection<ClothingItemDTO> GetItems()
        {
            return new ObservableCollection<ClothingItemDTO>(_clothingItemService.Get());
        }

        private RelayCommand _addCommand;
        public RelayCommand Add
        {
            get
            {
                return _addCommand ??
                  (_addCommand = new RelayCommand(obj =>
                  {
                      ClothingItemDTO item = obj as ClothingItemDTO;
                      if (item != null)
                      {
                          try
                          {
                              if (SelectedClothingItemType == null)
                              {
                                  throw new Exception("Выберите тип одежды");
                              }
                              if (SelectedManufacturer == null)
                              {
                                  throw new Exception("Выберите производителя");
                              }
                              item.ClothingItemTypeId = SelectedClothingItemType.Id;
                              item.ManufacturerId = SelectedManufacturer.Id;
                              _clothingItemService.Create(item);
                              ClothingItems = GetItems();
                          }
                          catch (Exception exception)
                          {
                              MessageBox.Show(exception.Message);
                          }
                      }
                  }));
            }
        }

        private RelayCommand _editCommand;
        public RelayCommand Edit
        {
            get
            {
                return _editCommand ??
                  (_editCommand = new RelayCommand(obj =>
                  {
                      ClothingItemDTO item = obj as ClothingItemDTO;
                      if (item != null && SelectedClothingItem != null)
                      {
                          try
                          {
                              if (SelectedClothingItemType == null)
                              {
                                  throw new Exception("Выберите тип одежды");
                              }
                              if (SelectedManufacturer == null)
                              {
                                  throw new Exception("Выберите производителя");
                              }
                              var updatedItem = new ClothingItemDTO
                              {
                                  Id = SelectedClothingItem.Id,
                                  Name = item.Name,
                                  Description = item.Description,
                                  Size = item.Size,
                                  IsMale = item.IsMale,
                                  Price = item.Price,
                                  ClothingItemTypeId = SelectedClothingItemType.Id,
                                  ManufacturerId = SelectedManufacturer.Id
                              };
                              _clothingItemService.Update(updatedItem);
                              ClothingItems = GetItems();
                          }
                          catch (Exception exception)
                          {
                              MessageBox.Show(exception.Message);
                          }
                      }
                  }));
            }
        }

        private RelayCommand _removeCommand;
        public RelayCommand Remove
        {
            get
            {
                return _removeCommand ??
                    (_removeCommand = new RelayCommand(obj =>
                    {
                        ClothingItemDTO item = obj as ClothingItemDTO;
                        if (item != null)
                        {
                            try
                            {
                                _clothingItemService.Delete(item.Id);
                                ClothingItems = GetItems();
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show(exception.Message);
                            }
                        }
                    }));
            }
        }

        private ObservableCollection<ManufacturerDTO> _manufacturers;
        public ObservableCollection<ManufacturerDTO> Manufacturers
        {
            get
            {
                _manufacturers = new ObservableCollection<ManufacturerDTO>(_manufacturerService.Get());
                return _manufacturers;
            }
        }

        private ObservableCollection<ClothingItemTypeDTO> _clothingItemTypes;
        public ObservableCollection<ClothingItemTypeDTO> ClothingItemTypes
        {
            get
            {
                _clothingItemTypes = new ObservableCollection<ClothingItemTypeDTO>(_clothingItemTypeService.Get());
                return _clothingItemTypes;
            }
        }

        private ManufacturerDTO _selectedManufacturer;
        public ManufacturerDTO SelectedManufacturer
        {
            get
            {
                return _selectedManufacturer;
            }
            set
            {
                _selectedManufacturer = value;
                OnPropertyChanged("SelectedManufacturer");
            }
        }

        private ClothingItemTypeDTO _selectedClothingItemType;
        public ClothingItemTypeDTO SelectedClothingItemType
        {
            get
            {
                return _selectedClothingItemType;
            }
            set
            {
                _selectedClothingItemType = value;
                OnPropertyChanged("SelectedClothingItemType");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
