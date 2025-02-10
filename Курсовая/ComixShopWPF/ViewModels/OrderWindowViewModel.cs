using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ComixShopWPF.Commands.@base;
using ComixShopWPF.Data;
using ComixShopWPF.Models;
using ComixShopWPF.Services;
using ComixShopWPF.ViewModels.Base;
using ComixShopWPF.Views.Windows;
using Microsoft.EntityFrameworkCore;

namespace ComixShopWPF.ViewModels
{
    public class OrderWindowViewModel: ViewModel
    {
        public string SelectedPaymentMethod { get; set; } = "Наличные";
        private string orderComment;
        public string OrderComment { get => orderComment; set { Set(ref orderComment, value); } }
        public string[] PaymentMethods { get; set; } = { "Наличные", "Карта", "Онлайн-оплата" };
        private ObservableCollection<CartItem> _cartItems;
        public ObservableCollection<CartItem> CartItems { get => _cartItems; set { Set(ref _cartItems, value); } }
        public decimal TotalCost => CartItems.Sum(item => item.Comix?.Price * item.Quantity ?? 0);
        public ICommand PlaceOrderCommand { get; }

        public OrderWindowViewModel()
        {
            LoadCartItems();
            PlaceOrderCommand = new RelayCommand(PlaceOrder);
        }
        private void LoadCartItems()
        {
            using (var context = new DataContext())
            {
                var userId = UserSession.Instance.CurrentUser.Id;
                CartItems = new ObservableCollection<CartItem>(
                    context.CartItems
                    .Where(ci => ci.Cart.UserId == userId)
                    .Include(ci => ci.Comix)
                    .ToList()
                );
            }
            OnPropertyChanged(nameof(TotalCost));
        }
        private void PlaceOrder(object? parm)
        {
            if (!CartItems.Any())
            {
                MessageBox.Show("Корзина пуста!");
                return;
            }

            using (var context = new DataContext())
            {
                var order = new Order
                {
                    UserId = UserSession.Instance.CurrentUser.Id,
                    PaymentMethod = SelectedPaymentMethod,
                    OrderStatus = "В ожидании",
                    OrderDate = DateTime.Now,
                    TotalCost = TotalCost,
                    OrderItems = CartItems.Select(item => new OrderItem
                    {
                        ComixId = item.ComixId,
                        Quantity = item.Quantity,
                        PriceAtPurchase = item.Comix.Price
                    }).ToList()
                };

                foreach (var item in order.OrderItems)
                {
                    var comix = context.Comixes.SingleOrDefault(c => c.Id == item.ComixId);
                    if (comix == null)
                    {
                        MessageBox.Show($"Комикс с ID {item.ComixId} не найден!");
                        return;
                    }

                    if (comix.StockQuantity < item.Quantity)
                    {
                        MessageBox.Show($"Недостаточно количества комикса '{comix.Title}' на складе!");
                        return;
                    }

                    comix.StockQuantity -= item.Quantity;
                }

                context.Orders.Add(order);
                context.CartItems.RemoveRange(context.CartItems.Where(ci => ci.Cart.UserId == UserSession.Instance.CurrentUser.Id));
                context.SaveChanges();
            }
            MessageBox.Show("Заказ оформлен!");
            CartItems.Clear();
            OnPropertyChanged(nameof(TotalCost));
            Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            currentWindow?.Close();
        }
    }
}
