using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComixShopWPF.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public ShoppingCart Cart { get; set; }
        public int ComixId { get; set; }
        public Comix Comix { get; set; }
        public int Quantity { get; set; }
    }
}

