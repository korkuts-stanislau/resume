using AutoMapper;
using ClothesShop.BLL.DTO;
using ClothesShop.BLL.Exceptions;
using ClothesShop.BLL.Interfaces.EntityServices;
using ClothesShop.DAL.Entities;
using ClothesShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesShop.BLL.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private IUnitOfWork _storage;

        public ManufacturerService(IUnitOfWork storage)
        {
            _storage = storage;
        }

        public void Create(ManufacturerDTO item)
        {
            try
            {
                Validate(item);
                Manufacturer man = new MapperConfiguration(cfg => cfg.CreateMap<ManufacturerDTO, Manufacturer>())
                    .CreateMapper()
                    .Map<Manufacturer>(item);
                _storage.Manufacturers.Create(man);
                _storage.Save();
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно добавить производителя. {exception.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                _storage.Manufacturers.Delete(id);
                _storage.Save();
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно удалить производителя. {exception.Message}");
            }
        }

        public IEnumerable<ManufacturerDTO> Get()
        {
            try
            {
                var manufacturers = _storage.Manufacturers.Get();
                return new MapperConfiguration(cfg => cfg.CreateMap<Manufacturer, ManufacturerDTO>())
                    .CreateMapper()
                    .Map<List<ManufacturerDTO>>(manufacturers);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить производителей. {exception.Message}");
            }
        }

        public ManufacturerDTO Get(int id)
        {
            try
            {
                var manufacturer = _storage.Manufacturers.Get(id);
                return new MapperConfiguration(cfg => cfg.CreateMap<ManufacturerDTO, Manufacturer>())
                    .CreateMapper()
                    .Map<ManufacturerDTO>(manufacturer);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить производителя. {exception.Message}");
            }
        }

        public void Update(ManufacturerDTO item)
        {
            try
            {
                Validate(item);
                var manufacturer = new MapperConfiguration(cfg => cfg.CreateMap<ManufacturerDTO, Manufacturer>())
                .CreateMapper()
                .Map<Manufacturer>(item);
                var man = _storage.Manufacturers.Find(m => m.Id == manufacturer.Id).FirstOrDefault();
                man.Name = manufacturer.Name;
                man.Description = manufacturer.Description;
                _storage.Manufacturers.Update(man);
                _storage.Save();
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно обновить производителя. {exception.Message}");
            }
        }

        private void Validate(ManufacturerDTO item)
        {
            if (item.Name == null || item.Name.Length < 4)
            {
                throw new ValidationException("Короткое имя производителя. Должно быть больше 3 символов", "Name");
            }

            if (item.Description == null || item.Description.Length < 4)
            {
                throw new ValidationException("Короткое описание производителя. Должно быть больше 3 символов", "Description");
            }
        }

        public IEnumerable<ManufacturerDTO> FilterByNameContainedText(string text)
        {
            var mans = _storage.Manufacturers.Find(man => man.Name.Contains(text));
            return new MapperConfiguration(cfg => cfg.CreateMap<Manufacturer, ManufacturerDTO>())
                    .CreateMapper()
                    .Map<List<ManufacturerDTO>>(mans);
        }
    }
}
