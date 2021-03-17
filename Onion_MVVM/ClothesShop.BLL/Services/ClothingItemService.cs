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
    public class ClothingItemService : IClothingItemService
    {
        private IUnitOfWork _storage;

        public ClothingItemService(IUnitOfWork storage)
        {
            _storage = storage;
        }

        public void Create(ClothingItemDTO item)
        {
            try
            {
                Validate(item);
                ClothingItem clothItem = new MapperConfiguration(cfg => cfg.CreateMap<ClothingItemDTO, ClothingItem>())
                    .CreateMapper()
                    .Map<ClothingItem>(item);
                _storage.ClothingItems.Create(clothItem);
                _storage.Save();
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно добавить одежду. {exception.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                _storage.ClothingItems.Delete(id);
                _storage.Save();
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно удалить одежду. {exception.Message}");
            }
        }

        public IEnumerable<ClothingItemDTO> Get()
        {
            try
            {
                var clothingItems = _storage.ClothingItems.Get();
                return new MapperConfiguration(cfg => cfg.CreateMap<ClothingItem, ClothingItemDTO>())
                    .CreateMapper()
                    .Map<List<ClothingItemDTO>>(clothingItems);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить одежду. {exception.Message}");
            }
        }

        public ClothingItemDTO Get(int id)
        {
            try
            {
                var clothingItem = _storage.ClothingItems.Get(id);
                return new MapperConfiguration(cfg => cfg.CreateMap<ClothingItemDTO, ClothingItem>())
                    .CreateMapper()
                    .Map<ClothingItemDTO>(clothingItem);
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно получить одежду. {exception.Message}");
            }
        }

        public void Update(ClothingItemDTO item)
        {
            try
            {
                Validate(item);
                var clothingItem = new MapperConfiguration(cfg => cfg.CreateMap<ClothingItemDTO, ClothingItem>())
                .CreateMapper()
                .Map<ClothingItem>(item);
                var clothItem = _storage.ClothingItems.Find(m => m.Id == clothingItem.Id).FirstOrDefault();

                clothItem.Name = clothingItem.Name;
                clothItem.Description = clothingItem.Description;
                clothItem.Size = clothingItem.Size;
                clothItem.IsMale = clothingItem.IsMale;
                clothItem.Price = clothingItem.Price;
                clothItem.ClothingItemTypeId = clothingItem.ClothingItemTypeId;
                clothItem.ManufacturerId = clothingItem.ManufacturerId;

                _storage.ClothingItems.Update(clothItem);
                _storage.Save();
            }
            catch (Exception exception)
            {
                throw new EntityServiceException($"Невозможно обновить одежду. {exception.Message}");
            }
        }
        public void Validate(ClothingItemDTO item)
        {
            if (item.Name == null || item.Name.Length < 4)
            {
                throw new ValidationException("Короткое название одежды. Должно быть больше 3 символов", "Name");
            }

            if (item.Description == null || item.Description.Length < 4)
            {
                throw new ValidationException("Короткое описание одежды. Должно быть больше 3 символов", "Description");
            }

            if (item.Size < 20 || item.Size > 70)
            {
                throw new ValidationException("Неверный размер. Размер одежды должен быть в диапазоне от 20 до 70.", "Size");
            }

            if (item.Price < 1)
            {
                throw new ValidationException("Неверная цена. Цена должна быть положительной", "Size");
            }

            try
            {
                _storage.ClothingItemTypes.Get(item.ClothingItemTypeId);
            }
            catch
            {
                throw new Exception("Нет такого типа одежды");
            }

            try
            {
                _storage.Manufacturers.Get(item.ManufacturerId);
            }
            catch
            {
                throw new Exception("Нет такого производителя");
            }
        }
    }
}
