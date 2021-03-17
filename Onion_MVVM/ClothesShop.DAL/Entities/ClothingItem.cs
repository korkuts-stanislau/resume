using System;
using System.Collections.Generic;
using System.Text;

namespace ClothesShop.DAL.Entities
{
    public class ClothingItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public bool IsMale { get; set; }
        public int Price { get; set; }

        public int ClothingItemTypeId { get; set; }
        public ClothingItemType ClothingItemType { get; set; }

        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
