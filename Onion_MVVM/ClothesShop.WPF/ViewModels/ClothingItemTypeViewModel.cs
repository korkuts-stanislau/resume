using ClothesShop.BLL.DTO;
using ClothesShop.BLL.Interfaces.EntityServices;
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
    public class ClothingItemTypeViewModel : IClothingItemTypeViewModel
    {
        private IClothingItemTypeService _clothingItemTypeService;
        public ClothingItemTypeViewModel(IClothingItemTypeService manService)
        {
            _clothingItemTypeService = manService;
            _bufMan = new ClothingItemTypeDTO();
        }

        private ClothingItemTypeDTO _bufMan;
        public ClothingItemTypeDTO BufferClothingItemType
        {
            get
            {
                return _bufMan;
            }
            set
            {
                _bufMan = value;
                OnPropertyChanged("BufferClothingItemType");
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

        private ObservableCollection<ClothingItemTypeDTO> _clothingItemTypes;
        public ObservableCollection<ClothingItemTypeDTO> ClothingItemTypes
        {
            get
            {
                _clothingItemTypes = GetClothingItemTypes();
                return _clothingItemTypes;
            }
            private set
            {
                _clothingItemTypes = value;
                OnPropertyChanged("ClothingItemTypes");
            }
        }

        private ObservableCollection<ClothingItemTypeDTO> GetClothingItemTypes()
        {
            return new ObservableCollection<ClothingItemTypeDTO>(_clothingItemTypeService.Get());
        }

        private RelayCommand _addCommand;
        public RelayCommand Add
        {
            get
            {
                return _addCommand ??
                  (_addCommand = new RelayCommand(obj =>
                  {
                      ClothingItemTypeDTO man = obj as ClothingItemTypeDTO;
                      if (man != null)
                      {
                          try
                          {
                              _clothingItemTypeService.Create(man);
                              ClothingItemTypes = GetClothingItemTypes();
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
                      ClothingItemTypeDTO man = obj as ClothingItemTypeDTO;
                      if (man != null && SelectedClothingItemType != null)
                      {
                          try
                          {
                              var updatedMan = new ClothingItemTypeDTO
                              {
                                  Id = SelectedClothingItemType.Id,
                                  Name = man.Name,
                                  Description = man.Description
                              };
                              _clothingItemTypeService.Update(updatedMan);
                              ClothingItemTypes = GetClothingItemTypes();
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
                        ClothingItemTypeDTO man = obj as ClothingItemTypeDTO;
                        if (man != null)
                        {
                            try
                            {
                                _clothingItemTypeService.Delete(man.Id);
                                ClothingItemTypes = GetClothingItemTypes();
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show(exception.Message);
                            }
                        }
                    }));
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
