using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.Data
{
    public class DbInitializer
    {
        int _refferenceTableSize;
        int _operationalTableSize;
        public DbInitializer(int refferenceTableSize = 100, int operationalTableSize = 10000)
        {
            _refferenceTableSize = refferenceTableSize;
            _operationalTableSize = operationalTableSize;
        }

        public async Task Initialize(ClothesShopContext dbContext)
        {
            Random rand = new Random();

            if(!dbContext.ClothingItemTypes.Any())
            {
                for(int i = 0; i < _refferenceTableSize; i++)
                {
                    await dbContext.ClothingItemTypes.AddAsync(new Models.ClothingItemType
                    {
                        Name = GetRandomString(50),
                        Description = GetRandomString(100)
                    });
                }
            }
            await dbContext.SaveChangesAsync();

            if (!dbContext.Manufacturers.Any())
            {
                for (int i = 0; i < _refferenceTableSize; i++)
                {
                    await dbContext.Manufacturers.AddAsync(new Models.Manufacturer
                    {
                        Name = GetRandomString(50),
                        Description = GetRandomString(100)
                    });
                }
            }
            await dbContext.SaveChangesAsync();

            if (!dbContext.ClothingItems.Any())
            {
                var types = await dbContext.ClothingItemTypes.ToListAsync();
                var manufacturers = await dbContext.Manufacturers.ToListAsync();

                for (int i = 0; i < _operationalTableSize; i++)
                {
                    var type = types.ElementAt(rand.Next(dbContext.ClothingItemTypes.Count() - 1));
                    var manufacturer = manufacturers.ElementAt(rand.Next(dbContext.Manufacturers.Count() - 1));

                    await dbContext.ClothingItems.AddAsync(new Models.ClothingItem
                    {
                        TypeId = type.Id,
                        ManufacturerId = manufacturer.Id,
                        Name = GetRandomString(50),
                        Description = GetRandomString(100),
                        Size = rand.Next(28, 58),
                        IsMale = rand.Next(0, 2) == 1,
                        Price = rand.Next(20, 1000)
                    });
                }
            }
            await dbContext.SaveChangesAsync();


        }

        public string GetRandomString(int maxLength)
        {
            Random rand = new Random();
            int length = rand.Next(maxLength / 3, maxLength);
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var str = new char[length];
            for (int i = 0; i < length; i++)
            {
                if ((i + 1) % 10 == 0)
                {
                    str[i] = ' ';
                    continue;
                }
                str[i] = chars[rand.Next(chars.Length)];
            }
            return new string(str);
        }
        public DateTime GetRandomDate(DateTime minDate, DateTime maxDate)
        {
            Random rand = new Random();
            int range = (maxDate - minDate).Days;
            return minDate.AddDays(rand.Next(range));
        }
    }
}
