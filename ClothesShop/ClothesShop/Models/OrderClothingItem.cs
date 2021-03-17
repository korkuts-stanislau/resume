using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.Models
{
    public class OrderClothingItem : Entity
    {
        [DisplayName("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [DisplayName("Clothes")]
        public int ClothingItemId { get; set; }
        public ClothingItem ClothingItem { get; set; }

        public int Count { get; set; }
    }
}
