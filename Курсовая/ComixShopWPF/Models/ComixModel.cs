using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ComixShopWPF.Models
{
    public class Comix
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        //public int AuthorId { get; set; }
        public string Author { get; set; }
        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    _price = 0;
                else
                    _price = value;
            }
        }
        private int _stockQuantity;
        public int StockQuantity
        {
            get => _stockQuantity;
            set
            {
                if (value < 0)
                    _stockQuantity = 0;
                else
                    _stockQuantity = value;
            }
        }
        public string ImageUrl { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public Comix(Comix comix)
        {
            Id = comix.Id;
            Title = comix.Title;
            Description = comix.Description;
            Genre = comix.Genre;
            Author = comix.Author;
            Price = comix.Price;
            StockQuantity = comix.StockQuantity;
            ImageUrl = comix.ImageUrl;
        }
        public Comix()
        {
            
        }
    }
}