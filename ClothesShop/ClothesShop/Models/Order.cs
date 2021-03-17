using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.Models
{
    public class Order : Entity
    {
        public string UserId { get; set; }
        [Required]
        [DisplayName("Customer name")]
        public string CustomerName { get; set; }
        [Required]
        [DisplayName("Customer phone")]
        public string CustomerPhone { get; set; }
        [Required]
        [DisplayName("Customer address")]
        public string CustomerAddress { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [DisplayName("Paid")]
        public bool IsPaid { get; set; }
        [Required]
        [DisplayName("Sent")]
        public bool IsSent { get; set; }
    }
}
