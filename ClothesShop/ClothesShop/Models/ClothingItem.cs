using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.Models
{
    public class ClothingItem : Entity
    {
        [DisplayName("Type")]
        public int TypeId { get; set; }
        public ClothingItemType Type { get; set; }

        [DisplayName("Manufacturer")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(30, 70, ErrorMessage = "Size should be in range from 30 to 70")]
        public int Size { get; set; }

        [DisplayName("Male")]
        public bool IsMale { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price should be positive")]
        public int Price { get; set; }
    }
}
