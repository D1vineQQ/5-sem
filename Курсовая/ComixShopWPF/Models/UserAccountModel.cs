using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComixShopWPF.Models
{

    public class UserAccount
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        //public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
