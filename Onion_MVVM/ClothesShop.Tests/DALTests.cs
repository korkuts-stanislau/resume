using ClothesShop.DAL.EF;
using ClothesShop.DAL.Entities;
using ClothesShop.DAL.Interfaces;
using ClothesShop.DAL.Repositories;
using ClothesShop.DAL.UnitsOfWork;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace ClothesShop.Tests
{
    public class DALTests
    {
        IUnitOfWork _storage;
        [SetUp]
        public void Setup()
        {
            var curDir = Directory.GetCurrentDirectory();
            var baseDir = Directory.GetParent(curDir).Parent.Parent;

            var config = new ConfigurationBuilder()
                .AddJsonFile(@$"{baseDir}\config.json")
                .Build();

            string connection = config["ConnectionStrings:MockDbConnection"];
            _storage = new ClothesShopDbUnitOfWork(connection);
        }

        [Test]
        public void ClothingItemTypeDbCreatingTest()
        {
            _storage.ClothingItemTypes.Create(new ClothingItemType
            {
                Name = "ClothingItemTypeTestName",
                Description = "ClothingItemTypeTestDescription"
            });
            _storage.Save();
            var type = _storage.ClothingItemTypes.Get().Last();
            Assert.AreEqual("ClothingItemTypeTestName", type.Name);
            Assert.AreEqual("ClothingItemTypeTestDescription", type.Description);
        }

        [Test]
        public void ClothingItemTypeDbDeletingTest()
        {
            _storage.ClothingItemTypes.Create(new ClothingItemType
            {
                Name = "ClothingItemTypeTestName",
                Description = "ClothingItemTypeTestDescription"
            });
            _storage.ClothingItemTypes.Create(new ClothingItemType
            {
                Name = "ClothingItemTypeDeleteName",
                Description = "ClothingItemTypeDeleteDescription"
            });
            _storage.Save();
            var type = _storage.ClothingItemTypes.Get().Last();
            Assert.AreEqual("ClothingItemTypeDeleteName", type.Name);
            Assert.AreEqual("ClothingItemTypeDeleteDescription", type.Description);

            _storage.ClothingItemTypes.Delete(type.Id);
            _storage.Save();
            Assert.AreNotEqual(type.Id, _storage.ClothingItemTypes.Get().Last().Id);
        }

        [Test]
        public void ClothingItemTypeDbUpdatingTest()
        {
            _storage.ClothingItemTypes.Create(new ClothingItemType
            {
                Name = "UpdateMe",
                Description = "Please"
            });
            _storage.Save();
            var type = _storage.ClothingItemTypes.Get().Last();
            Assert.AreEqual("UpdateMe", type.Name);
            Assert.AreEqual("Please", type.Description);

            type.Name = "Yeah";
            type.Description = "Thank you";

            _storage.ClothingItemTypes.Update(type);
            _storage.Save();

            type = _storage.ClothingItemTypes.Get().Last();

            Assert.AreEqual("Yeah", type.Name);
            Assert.AreEqual("Thank you", type.Description);
        }


        [Test]
        public void ClothingItemTypeDbFilteringTest()
        {
            _storage.ClothingItemTypes.Create(new ClothingItemType
            {
                Name = "Filter",
                Description = "Empty"
            });
            _storage.ClothingItemTypes.Create(new ClothingItemType
            {
                Name = "Filter",
                Description = "Empty"
            });
            _storage.ClothingItemTypes.Create(new ClothingItemType
            {
                Name = "Filter",
                Description = "Empty"
            });
            _storage.ClothingItemTypes.Create(new ClothingItemType
            {
                Name = "Not a filter",
                Description = "Empty"
            });
            _storage.Save();

            var types = _storage.ClothingItemTypes.Find(item => item.Name == "Filter");
            Assert.AreEqual(3, types.Count());

            foreach(var type in types)
            {
                _storage.ClothingItemTypes.Delete(type.Id);
            }
            _storage.Save();

            types = _storage.ClothingItemTypes.Find(item => item.Name == "Filter");
            Assert.AreEqual(0, types.Count());
        }


        [Test]
        public void ManufacturerDbCreatingTest()
        {
            _storage.Manufacturers.Create(new Manufacturer
            {
                Name = "ManufacturerTestName",
                Description = "ManufacturerTestDescription"
            });
            _storage.Save();
            var man = _storage.Manufacturers.Get().Last();
            Assert.AreEqual("ManufacturerTestName", man.Name);
            Assert.AreEqual("ManufacturerTestDescription", man.Description);
        }

        [Test]
        public void ManufacturerDbDeletingTest()
        {
            _storage.Manufacturers.Create(new Manufacturer
            {
                Name = "ManufacturerTestName",
                Description = "ManufacturerTestDescription"
            });
            _storage.Manufacturers.Create(new Manufacturer
            {
                Name = "ManufacturerDeleteName",
                Description = "ManufacturerDeleteDescription"
            });
            _storage.Save();
            var man = _storage.Manufacturers.Get().Last();
            Assert.AreEqual("ManufacturerDeleteName", man.Name);
            Assert.AreEqual("ManufacturerDeleteDescription", man.Description);

            _storage.Manufacturers.Delete(man.Id);
            _storage.Save();
            Assert.AreNotEqual(man.Id, _storage.Manufacturers.Get().Last().Id);
        }

        [Test]
        public void ManufacturerDbUpdatingTest()
        {
            _storage.Manufacturers.Create(new Manufacturer
            {
                Name = "UpdateMe",
                Description = "Please"
            });
            _storage.Save();
            var man = _storage.Manufacturers.Get().Last();
            Assert.AreEqual("UpdateMe", man.Name);
            Assert.AreEqual("Please", man.Description);

            man.Name = "Yeah";
            man.Description = "Thank you";

            _storage.Manufacturers.Update(man);
            _storage.Save();

            man = _storage.Manufacturers.Get().Last();

            Assert.AreEqual("Yeah", man.Name);
            Assert.AreEqual("Thank you", man.Description);
        }


        [Test]
        public void ManufacturerDbFilteringTest()
        {
            var mans = _storage.Manufacturers.Find(item => item.Name == "Filter");

            foreach (var man in mans)
            {
                _storage.Manufacturers.Delete(man.Id);
            }
            _storage.Save();

            _storage.Manufacturers.Create(new Manufacturer
            {
                Name = "Filter",
                Description = "Empty"
            });
            _storage.Manufacturers.Create(new Manufacturer
            {
                Name = "Filter",
                Description = "Empty"
            });
            _storage.Manufacturers.Create(new Manufacturer
            {
                Name = "Filter",
                Description = "Empty"
            });
            _storage.Manufacturers.Create(new Manufacturer
            {
                Name = "Not a filter",
                Description = "Empty"
            });
            _storage.Save();

            mans = _storage.Manufacturers.Find(item => item.Name == "Filter");
            Assert.AreEqual(3, mans.Count());

            foreach (var man in mans)
            {
                _storage.Manufacturers.Delete(man.Id);
            }
            _storage.Save();

            mans = _storage.Manufacturers.Find(item => item.Name == "Filter");
            Assert.AreEqual(0, mans.Count());
        }

        [Test]
        public void ClothingItemDbCreatingTest()
        {
            _storage.ClothingItemTypes.Create(new ClothingItemType
            {
                Name = "ClothingItemTypeTestName",
                Description = "ClothingItemTypeTestDescription"
            });

            _storage.Manufacturers.Create(new Manufacturer
            {
                Name = "ManufacturerTestName",
                Description = "ManufacturerTestName"
            });

            _storage.Save();

            var type = _storage.ClothingItemTypes.Get().LastOrDefault();
            var man = _storage.Manufacturers.Get().LastOrDefault();

            _storage.ClothingItems.Create(new ClothingItem
            {
                Name = "ClothingItemTestName",
                Description = "ClothingItemTestDescription",
                Size = 30,
                IsMale = true,
                Price = 111,
                ClothingItemTypeId = type.Id,
                ManufacturerId = man.Id
            });
            _storage.Save();
            var item = _storage.ClothingItems.Get().Last();
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
            _storage.ClothingItemTypes.Create(new ClothingItemType
            {
                Name = "ClothingItemTypeTestName",
                Description = "ClothingItemTypeTestDescription"
            });

            _storage.Manufacturers.Create(new Manufacturer
            {
                Name = "ManufacturerTestName",
                Description = "ManufacturerTestName"
            });

            _storage.Save();

            var type = _storage.ClothingItemTypes.Get().LastOrDefault();
            var man = _storage.Manufacturers.Get().LastOrDefault();

            _storage.ClothingItems.Create(new ClothingItem
            {
                Name = "ClothingItemTestName",
                Description = "ClothingItemTestDescription",
                Size = 30,
                IsMale = true,
                Price = 111,
                ClothingItemTypeId = type.Id,
                ManufacturerId = man.Id
            });
            _storage.Save();
            _storage.ClothingItems.Create(new ClothingItem
            {
                Name = "ClothingItemDeleteName",
                Description = "ClothingItemDeleteDescription",
                Size = 30,
                IsMale = true,
                Price = 111,
                ClothingItemTypeId = type.Id,
                ManufacturerId = man.Id
            });
            _storage.Save();
            var item = _storage.ClothingItems.Get().Last();
            Assert.AreEqual("ClothingItemDeleteName", item.Name);
            Assert.AreEqual("ClothingItemDeleteDescription", item.Description);

            _storage.ClothingItems.Delete(item.Id);
            _storage.Save();
            Assert.AreNotEqual(item.Id, _storage.ClothingItems.Get().Last().Id);
        }

        [Test]
        public void ClothingItemDbUpdatingTest()
        {
            _storage.ClothingItemTypes.Create(new ClothingItemType
            {
                Name = "ClothingItemTypeTestName",
                Description = "ClothingItemTypeTestDescription"
            });

            _storage.Manufacturers.Create(new Manufacturer
            {
                Name = "ManufacturerTestName",
                Description = "ManufacturerTestName"
            });

            _storage.Save();

            var type = _storage.ClothingItemTypes.Get().LastOrDefault();
            var man = _storage.Manufacturers.Get().LastOrDefault();

            _storage.ClothingItems.Create(new ClothingItem
            {
                Name = "UpdateMe",
                Description = "Please",
                Size = 30,
                IsMale = true,
                Price = 111,
                ClothingItemTypeId = type.Id,
                ManufacturerId = man.Id
            });
            _storage.Save();
            var item = _storage.ClothingItems.Get().Last();
            Assert.AreEqual("UpdateMe", item.Name);
            Assert.AreEqual("Please", item.Description);

            item.Name = "Yeah";
            item.Description = "Thank you";

            _storage.ClothingItems.Update(item);
            _storage.Save();

            item = _storage.ClothingItems.Get().Last();

            Assert.AreEqual("Yeah", item.Name);
            Assert.AreEqual("Thank you", item.Description);
        }


        [Test]
        public void ClothingItemDbFilteringTest()
        {
            _storage.ClothingItemTypes.Create(new ClothingItemType
            {
                Name = "ClothingItemTypeTestName",
                Description = "ClothingItemTypeTestDescription"
            });

            _storage.Manufacturers.Create(new Manufacturer
            {
                Name = "ManufacturerTestName",
                Description = "ManufacturerTestName"
            });

            _storage.Save();

            var type = _storage.ClothingItemTypes.Get().LastOrDefault();
            var man = _storage.Manufacturers.Get().LastOrDefault();

            _storage.ClothingItems.Create(new ClothingItem
            {
                Name = "Filter",
                Description = "Empty",
                Size = 30,
                IsMale = true,
                Price = 111,
                ClothingItemTypeId = type.Id,
                ManufacturerId = man.Id
            });
            _storage.ClothingItems.Create(new ClothingItem
            {
                Name = "Filter",
                Description = "Empty",
                Size = 30,
                IsMale = true,
                Price = 111,
                ClothingItemTypeId = type.Id,
                ManufacturerId = man.Id
            });
            _storage.ClothingItems.Create(new ClothingItem
            {
                Name = "Filter",
                Description = "Empty",
                Size = 30,
                IsMale = true,
                Price = 111,
                ClothingItemTypeId = type.Id,
                ManufacturerId = man.Id
            });
            _storage.ClothingItems.Create(new ClothingItem
            {
                Name = "Not a filter",
                Description = "Empty",
                Size = 30,
                IsMale = true,
                Price = 111,
                ClothingItemTypeId = type.Id,
                ManufacturerId = man.Id
            });
            _storage.Save();

            var items = _storage.ClothingItems.Find(item => item.Name == "Filter");
            Assert.AreEqual(3, items.Count());

            foreach (var item in items)
            {
                _storage.ClothingItems.Delete(item.Id);
            }
            _storage.Save();

            items = _storage.ClothingItems.Find(item => item.Name == "Filter");
            Assert.AreEqual(0, items.Count());
        }
    }
}