using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComixShopWPF.Commands.@base;
using ComixShopWPF.Data;
using ComixShopWPF.Models;
using ComixShopWPF.Services;
using System.Windows.Input;
using System.Windows;
using ComixShopWPF.ViewModels.Base;
using ComixShopWPF.Views.Windows;
using Microsoft.EntityFrameworkCore;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace ComixShopWPF.ViewModels
{
    public class CartViewModel : ViewModel
    {
        private ObservableCollection<CartItem> _cartItems;
        public ObservableCollection<CartItem> CartItems { get => _cartItems; set { Set(ref _cartItems, value); } }

        // Общая стоимость корзины
        public decimal TotalCost => CartItems.Sum(item => item.Comix?.Price * item.Quantity ?? 0);

        // Команды для кнопок
        public ICommand RemoveItemCommand { get; }
        public ICommand PlaceOrderCommand { get; }
        public ICommand ClearCartCommand { get; }

        public CartViewModel()
        {
            LoadCartItems();

            RemoveItemCommand = new RelayCommand(RemoveItem);
            PlaceOrderCommand = new RelayCommand(PlaceOrder);
            ClearCartCommand = new RelayCommand(ClearCart);
        }

        private void ClearCart(object? parm)
        {
            using (var context = new DataContext())
            {
                // Получаем все элементы корзины
                var allItems = context.CartItems.ToList();

                // Удаляем все элементы из контекста
                context.CartItems.RemoveRange(allItems);

                // Сохраняем изменения в базе данных
                context.SaveChanges();
            }

            CartItems.Clear();
            OnPropertyChanged(nameof(TotalCost));
        }

        //private void LoadCartItems()
        //{
        //    using (var context = new DataContext())
        //    {
        //        var userId = UserSession.Instance.CurrentUser.Id;

        //        CartItems = new ObservableCollection<CartItem>(
        //            context.CartItems
        //            .Where(ci => ci.Cart.UserId == userId)
        //            .ToList()
        //        );
        //    }
        //    OnPropertyChanged(nameof(TotalCost));
        //}
        private void LoadCartItems()
        {
            using (var context = new DataContext())
            {
                var userId = UserSession.Instance.CurrentUser.Id;

                // Include Comix data to ensure it is loaded with CartItems
                CartItems = new ObservableCollection<CartItem>(
                    context.CartItems
                    .Where(ci => ci.Cart.UserId == userId)
                    .Include(ci => ci.Comix)  // Ensure Comix is included
                    .ToList()
                );
            }
            OnPropertyChanged(nameof(TotalCost));
        }


        private void RemoveItem(object objItem)
        {
            if (objItem == null) return;
            CartItem item = (CartItem)objItem;
            

            using (var context = new DataContext())
            {
                var itemToRemove = context.CartItems.FirstOrDefault(ci => ci.Id == item.Id);
                if (itemToRemove != null)
                {
                    context.CartItems.Remove(itemToRemove);
                    context.SaveChanges();
                }
            }

            CartItems.Remove(item);
            OnPropertyChanged(nameof(TotalCost));
        }

        private void PlaceOrder(object? parm)
        {
            if (!CartItems.Any())
            {
                MessageBox.Show("Корзина пуста!");
                return;
            }

            //using (var context = new DataContext())
            //{
            //    var order = new Order
            //    {
            //        UserId = UserSession.Instance.CurrentUser.Id,
            //        PaymentMethod = "Наличные",
            //        OrderStatus = "В ожидании",
            //        OrderDate = DateTime.Now,
            //        TotalCost = TotalCost,
            //        OrderItems = CartItems.Select(item => new OrderItem
            //        {
            //            ComixId = item.ComixId,
            //            Quantity = item.Quantity,
            //            PriceAtPurchase = item.Comix.Price
            //        }).ToList()
            //    };

            //    context.Orders.Add(order);
            //    context.CartItems.RemoveRange(context.CartItems.Where(ci => ci.Cart.UserId == UserSession.Instance.CurrentUser.Id));
            //    context.SaveChanges();
            //}

            //MessageBox.Show("Заказ оформлен!");
            //CartItems.Clear();
            //OnPropertyChanged(nameof(TotalCost));

            Window window = new OrderWindow();
            window.ShowDialog();
            CartItems.Clear();
            //OnPropertyChanged(nameof);
        }
    }
}
