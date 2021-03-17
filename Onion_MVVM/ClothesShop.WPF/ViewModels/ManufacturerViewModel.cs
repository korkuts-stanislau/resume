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
using System.Windows.Data;

namespace ClothesShop.WPF.ViewModels
{
    public class ManufacturerViewModel : IManufacturerViewModel
    {
        private IManufacturerService _manufacturerService;
        public ManufacturerViewModel(IManufacturerService manService)
        {
            _manufacturerService = manService;
            _bufMan = new ManufacturerDTO();
        }

        private ManufacturerDTO _bufMan;
        public ManufacturerDTO BufferManufacturer
        {
            get
            {
                return _bufMan;
            }
            set
            {
                _bufMan = value;
                OnPropertyChanged("BufferManufacturer");
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

        private ObservableCollection<ManufacturerDTO> _manufacturers;
        public ObservableCollection<ManufacturerDTO> Manufacturers
        {
            get
            {
                _manufacturers = GetManufacturers();
                return _manufacturers;
            }
            private set
            {
                _manufacturers = value;
                OnPropertyChanged("Manufacturers");
            }
        }

        private ObservableCollection<ManufacturerDTO> GetManufacturers()
        {
            return new ObservableCollection<ManufacturerDTO>(_manufacturerService.Get());
        }

        private RelayCommand _addCommand;
        public RelayCommand Add
        {
            get
            {
                return _addCommand ??
                  (_addCommand = new RelayCommand(obj =>
                  {
                      ManufacturerDTO man = obj as ManufacturerDTO;
                      if (man != null)
                      {
                          try
                          {
                              _manufacturerService.Create(man);
                              Manufacturers = GetManufacturers();
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
                      ManufacturerDTO man = obj as ManufacturerDTO;
                      if (man != null && SelectedManufacturer != null)
                      {
                          try
                          {
                              var updatedMan = new ManufacturerDTO
                              {
                                  Id = SelectedManufacturer.Id,
                                  Name = man.Name,
                                  Description = man.Description
                              };
                              _manufacturerService.Update(updatedMan);
                              Manufacturers = GetManufacturers();
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
                        ManufacturerDTO man = obj as ManufacturerDTO;
                        if (man != null)
                        {
                            try
                            {
                                _manufacturerService.Delete(man.Id);
                                Manufacturers = GetManufacturers();
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
