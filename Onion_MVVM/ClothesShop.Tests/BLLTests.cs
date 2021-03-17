using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;
using ClothesShop.DAL.Interfaces;
using ClothesShop.DAL.UnitsOfWork;
using ClothesShop.BLL.Interfaces.EntityServices;
using ClothesShop.BLL.Services;
using ClothesShop.BLL.DTO;

namespace ClothesShop.Tests
{
    class BLLTests
    {
        IManufacturerService _manufacturers;
        IClothingItemTypeService _itemTypes;
        IClothingItemService _items;

        [SetUp]
        public void Setup()
        {
            var curDir = Directory.GetCurrentDirectory();
            var baseDir = Directory.GetParent(curDir).Parent.Parent;

            var config = new ConfigurationBuilder()
                .AddJsonFile(@$"{baseDir}\config.json")
                .Build();

            string connection = config["ConnectionStrings:MockDbConnection"];
            IUnitOfWork _storage = new ClothesShopDbUnitOfWork(connection);
            _manufacturers = new ManufacturerService(_storage);
            _itemTypes = new ClothingItemTypeService(_storage);
            _items = new ClothingItemService(_storage);
        }


        [Test]
        public void ClothingItemTypeDbCreatingTest()
        {
            _itemTypes.Create(new ClothingItemTypeDTO
            {
                Name = "ClothingItemTypeTestName",
                Description = "ClothingItemTypeTestDescription"
            });
            var type = _itemTypes.Get().Last();
            Assert.AreEqual("ClothingItemTypeTestName", type.Name);
            Assert.AreEqual("ClothingItemTypeTestDescription", type.Description);
        }

        [Test]
        public void ClothingItemTypeDbDeletingTest()
        {
            _itemTypes.Create(new ClothingItemTypeDTO
            {
                Name = "ClothingItemTypeTestName",
                Description = "ClothingItemTypeTestDescription"
            });
            _itemTypes.Create(new ClothingItemTypeDTO
            {
                Name = "ClothingItemTypeDeleteName",
                Description = "ClothingItemTypeDeleteDescription"
            });

            var type = _itemTypes.Get().Last();
            Assert.AreEqual("ClothingItemTypeDeleteName", type.Name);
            Assert.AreEqual("ClothingItemTypeDeleteDescription", type.Description);

            _itemTypes.Delete(type.Id);
            Assert.AreNotEqual(type.Id, _itemTypes.Get().Last().Id);
        }

        [Test]
        public void ClothingItemTypeDbUpdatingTest()
        {
            _itemTypes.Create(new ClothingItemTypeDTO
            {
                Name = "UpdateMe",
                Description = "Please"
            });
            var type = _itemTypes.Get().Last();
            Assert.AreEqual("UpdateMe", type.Name);
            Assert.AreEqual("Please", type.Description);

            type.Name = "Yeah";
            type.Description = "Thank you";

            _itemTypes.Update(type);

            type = _itemTypes.Get().Last();

            Assert.AreEqual("Yeah", type.Name);
            Assert.AreEqual("Thank you", type.Description);
        }


        [Test]
        public void ManufacturerDbCreatingTest()
        {
            _manufacturers.Create(new ManufacturerDTO
            {
                Name = "ManufacturerTestName",
                Description = "ManufacturerTestDescription"
            });
            var man = _manufacturers.Get().Last();
            Assert.AreEqual("ManufacturerTestName", man.Name);
            Assert.AreEqual("ManufacturerTestDescription", man.Description);
        }

        [Test]
        public void ManufacturerDbDeletingTest()
        {
            _manufacturers.Create(new ManufacturerDTO
            {
                Name = "ManufacturerTestName",
                Description = "ManufacturerTestDescription"
            });
            _manufacturers.Create(new ManufacturerDTO
            {
                Name = "ManufacturerDeleteName",
                Description = "ManufacturerDeleteDescription"
            });
            var man = _manufacturers.Get().Last();
            Assert.AreEqual("ManufacturerDeleteName", man.Name);
            Assert.AreEqual("ManufacturerDeleteDescription", man.Description);

            _manufacturers.Delete(man.Id);
            Assert.AreNotEqual(man.Id, _manufacturers.Get().Last().Id);
        }

        [Test]
        public void ManufacturerDbUpdatingTest()
        {
            _manufacturers.Create(new ManufacturerDTO
            {
                Name = "UpdateMe",
                Description = "Please"
            });
            var man = _manufacturers.Get().Last();
            Assert.AreEqual("UpdateMe", man.Name);
            Assert.AreEqual("Please", man.Description);

            man.Name = "Yeah";
            man.Description = "Thank you";

            _manufacturers.Update(man);

            man = _manufacturers.Get().Last();

            Assert.AreEqual("Yeah", man.Name);
            Assert.AreEqual("Thank you", man.Description);
        }

        [Test]
        public void ClothingItemDbCreatingTest()
        {
            _itemTypes.Create(new ClothingItemTypeDTO
            {
                Name = "ClothingItemTypeTestName",
                Description = "ClothingItemTypeTestDescription"
            });

            _manufacturers.Create(new ManufacturerDTO
            {
                Name = "ManufacturerTestName",
                Description = "ManufacturerTestName"
            });


            var type = _itemTypes.Get().LastOrDefault();
            var man = _manufacturers.Get().LastOrDefault();

            _items.Create(new ClothingItemDTO
            {
                Name = "ClothingItemTestName",
                Description = "ClothingItemTestDescription",
                Size = 30,
                IsMale = true,
                Price = 111,
                ClothingItemTypeId = type.Id,
                ManufacturerId = man.Id
            });

            var item = _items.Get().Last();
            Assert.AreEqual("ClothingItemTestName", item.Name);
            Assert.AreEqual("ClothingItemTestDescription", item.Description);
            Assert.AreEqual(30, item.Size);
            Assert.AreEqual(true, item.IsMale);
            Assert.AreEqual(111, item.Price, 2);
            Assert.AreEqual(type.Id, item.ClothingItemTypeId);
            Assert.AreEqual(man.Id, item.ManufacturerId);
        }

        [Test]
        public void ClothingItemDbDeletingTest()
        {
            _itemTypes.Create(new ClothingItemTypeDTO
            {
                Name = "ClothingItemTypeTestName",
                Description = "ClothingItemTypeTestDescription"
            });

            _manufacturers.Create(new ManufacturerDTO
            {
                Name = "ManufacturerTestName",
                Description = "ManufacturerTestName"
            });


            var type = _itemTypes.Get().LastOrDefault();
            var man = _manufacturers.Get().LastOrDefault();

            _items.Create(new ClothingItemDTO
            {
                Name = "ClothingItemTestName",
                Description = "ClothingItemTestDescription",
                Size = 30,
                IsMale = true,
                Price = 111,
                ClothingItemTypeId = type.Id,
                ManufacturerId = man.Id
            });
            _items.Create(new ClothingItemDTO
            {
                Name = "ClothingItemDeleteName",
                Description = "ClothingItemDeleteDescription",
                Size = 30,
                IsMale = true,
                Price = 111,
                ClothingItemTypeId = type.Id,
                ManufacturerId = man.Id
            });
            var item = _items.Get().Last();
            Assert.AreEqual("ClothingItemDeleteName", item.Name);
            Assert.AreEqual("ClothingItemDeleteDescription", item.Description);

            _items.Delete(item.Id);
            Assert.AreNotEqual(item.Id, _items.Get().Last().Id);
        }

        [Test]
        public void ClothingItemDbUpdatingTest()
        {
            _itemTypes.Create(new ClothingItemTypeDTO
            {
                Name = "ClothingItemTypeTestName",
                Description = "ClothingItemTypeTestDescription"
            });

            _manufacturers.Create(new ManufacturerDTO
            {
                Name = "ManufacturerTestName",
                Description = "ManufacturerTestName"
            });


            var type = _itemTypes.Get().LastOrDefault();
            var man = _manufacturers.Get().LastOrDefault();

            _items.Create(new ClothingItemDTO
            {
                Name = "UpdateMe",
                Description = "Please",
                Size = 30,
                IsMale = true,
                Price = 111,
                ClothingItemTypeId = type.Id,
                ManufacturerId = man.Id
            });
            var item = _items.Get().Last();
            Assert.AreEqual("UpdateMe", item.Name);
            Assert.AreEqual("Please", item.Description);

            item.Name = "Yeah";
            item.Description = "Thank you";

            _items.Update(item);

            item = _items.Get().Last();

            Assert.AreEqual("Yeah", item.Name);
            Assert.AreEqual("Thank you", item.Description);
        }
    }
}
