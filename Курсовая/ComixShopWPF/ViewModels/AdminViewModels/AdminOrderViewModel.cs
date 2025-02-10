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
using ComixShopWPF.ViewModels.Base;
using Microsoft.EntityFrameworkCore;

namespace ComixShopWPF.ViewModels.AdminViewModels
{
    public class AdminOrderViewModel:ViewModel
    {
        //private UserAccount _userAccount;
        //public UserAccount CurrentUser { get => _userAccount; set { Set(ref _userAccount, value); } }
        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set { Set(ref _orders, value); }
        }

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                Set(ref _selectedOrder, value);
                LoadOrderItems(value);
            }
        }

        private ObservableCollection<OrderItem> _orderItems;
        public ObservableCollection<OrderItem> OrderItems
        {
            get => _orderItems;
            set { Set(ref _orderItems, value); }
        }

        public ICommand RefreshCommand { get; }
        public ICommand ConfirmOrder { get; }
        public ICommand DeclineOrder { get; }
        public AdminOrderViewModel()
        {
            //if (UserSession.Instance.IsAuthenticated)
            //    CurrentUser = UserSession.Instance.CurrentUser;
            LoadOrders(null);

            RefreshCommand = new RelayCommand(LoadOrders);
            ConfirmOrder = new RelayCommand(conOrders);
            DeclineOrder = new RelayCommand(declOrders);
        }

        private void conOrders(object? parm)
        {
            if (SelectedOrder == null) return;

            using (var context = new DataContext())
            {
                var order = context.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == SelectedOrder.Id);
                if (order != null)
                {
                    order.OrderStatus = "Confirmed"; // Update order status to confirmed
                    context.SaveChanges();
                }
            }

            LoadOrders(null); // Refresh the order list
        }
        private void declOrders(object? parm)
        {
            if (SelectedOrder == null) return;

            using (var context = new DataContext())
            {
                var order = context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Comix)
                    .FirstOrDefault(o => o.Id == SelectedOrder.Id);

                if (order != null)
                {
                    // Change the order status to "Declined"
                    order.OrderStatus = "Declined";

                    // Restore the StockQuantity of each comic in the order
                    foreach (var orderItem in order.OrderItems)
                    {
                        var comix = orderItem.Comix;
                        comix.StockQuantity += orderItem.Quantity; // Restore stock
                    }

                    context.SaveChanges();
                    OnPropertyChanged(nameof(OrderItems));
                }
            }

            LoadOrders(null); // Refresh the order list
        }


        private void LoadOrderItems(Order selectedOrder)
        {
            if (selectedOrder == null) return;

            using (var context = new DataContext())
            {
                OrderItems = new ObservableCollection<OrderItem>(
                    context.OrderItems
                        .Where(oi => oi.OrderId == selectedOrder.Id)
                        .Include(oi => oi.Comix)
                        .ToList()
                );
            }
        }

        private void LoadOrders(object? parm)
        {
            using (var context = new DataContext())
            {
                //var userId = UserSession.Instance.CurrentUser.Id;
                Orders = new ObservableCollection<Order>(
                    context.Orders
                        .Include(o => o.User)
                        .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Comix)
                        .OrderByDescending(o => o.OrderDate)
                        .ToList()
                );
            }
        }
    }
}
