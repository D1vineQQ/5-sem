using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComixShopWPF.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserAccount User { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalCost { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
