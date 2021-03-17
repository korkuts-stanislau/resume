using System;
using System.Collections.Generic;
using System.Text;

namespace ClothesShop.BLL.DTO
{
    public class ClothingItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public bool IsMale { get; set; }
        public int Price { get; set; }

        public int ClothingItemTypeId { get; set; }

        public int ManufacturerId { get; set; }
    }
}
