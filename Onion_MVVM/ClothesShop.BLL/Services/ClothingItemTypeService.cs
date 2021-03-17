using AutoMapper;
using ClothesShop.BLL.DTO;
using ClothesShop.BLL.Exceptions;
using ClothesShop.BLL.Interfaces;
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
    public class ClothingItemTypeService : IClothingItemTypeService
    {
        private IUnitOfWork _storage;

        public ClothingItemTypeService(IUnitOfWork storage)
        {
            _storage = storage;
        }

        public void Create(ClothingItemTypeDTO item)
        {
            try
            {
                Validate(item);
                ClothingItemType type = new MapperConfiguration(cfg => cfg.CreateMap<ClothingItemTypeDTO, ClothingItemType>())
                    .CreateMapper()
                    .Map<ClothingItemType>(item);
                _storage.ClothingItemTypes.Create(type);
                _storage.Save();
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно добавить тип одежды. {exception.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                _storage.ClothingItemTypes.Delete(id);
                _storage.Save();
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно удалить тип одежды. {exception.Message}");
            }
        }

        public IEnumerable<ClothingItemTypeDTO> Get()
        {
            try
            {
                var types = _storage.ClothingItemTypes.Get();
                return new MapperConfiguration(cfg => cfg.CreateMap<ClothingItemType, ClothingItemTypeDTO>())
                    .CreateMapper()
                    .Map<List<ClothingItemTypeDTO>>(types);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить типы одежды. {exception.Message}");
            }
        }

        public ClothingItemTypeDTO Get(int id)
        {
            try
            {
                var type = _storage.ClothingItemTypes.Get(id);
                return new MapperConfiguration(cfg => cfg.CreateMap<ClothingItemTypeDTO, ClothingItemType>())
                    .CreateMapper()
                    .Map<ClothingItemTypeDTO>(type);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить тип одежды. {exception.Message}");
            }
        }

        public void Update(ClothingItemTypeDTO item)
        {
            try
            {
                Validate(item);
                var type = new MapperConfiguration(cfg => cfg.CreateMap<ClothingItemTypeDTO, ClothingItemType>())
                .CreateMapper()
                .Map<ClothingItemType>(item);
                var itemType = _storage.ClothingItemTypes.Find(m => m.Id == type.Id).FirstOrDefault();
                itemType.Name = type.Name;
                itemType.Description = type.Description;
                _storage.ClothingItemTypes.Update(itemType);
                _storage.Save();
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно обновить тип одежды. {exception.Message}");
            }
        }

        private void Validate(ClothingItemTypeDTO item)
        {
            if (item.Name == null || item.Name.Length < 4)
            {
                throw new ValidationException("Короткое название типа одежды. Должно быть больше 3 символов", "Name");
            }

            if (item.Description == null || item.Description.Length < 4)
            {
                throw new ValidationException("Короткое описание типа одежды. Должно быть больше 3 символов", "Description");
            }
        }
    }
}
