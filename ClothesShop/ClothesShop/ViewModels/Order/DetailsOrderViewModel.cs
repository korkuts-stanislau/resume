using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.ViewModels.Order
{
    public class DetailsOrderViewModel
    {
        public Models.Order Order { get; set; }
        public IEnumerable<Models.OrderClothingItem> OrderClothingItems { get; set; }

        [DisplayName("Total price")]
        public int TotalPrice
        {
            get
            {
                return OrderClothingItems
                    .Select(o => o.ClothingItem.Price * o.Count)
                    .Sum();
            }
        }
    }
}
