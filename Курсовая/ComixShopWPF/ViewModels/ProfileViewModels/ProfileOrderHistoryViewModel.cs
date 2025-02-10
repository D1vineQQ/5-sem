using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComixShopWPF.Models;
using System.Windows.Input;
using ComixShopWPF.ViewModels.Base;
using ComixShopWPF.Commands.@base;
using ComixShopWPF.Services;
using ComixShopWPF.Data;
using Microsoft.EntityFrameworkCore;

namespace ComixShopWPF.ViewModels.ProfileViewModels
{
    public class ProfileOrderHistoryViewModel:ViewModel
    {
        private UserAccount _userAccount;
        public UserAccount CurrentUser { get => _userAccount; set { Set(ref _userAccount, value); } }
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
        public ProfileOrderHistoryViewModel()
        {
            if (UserSession.Instance.IsAuthenticated)
                CurrentUser = UserSession.Instance.CurrentUser;
            LoadOrders(null);

            RefreshCommand = new RelayCommand(LoadOrders);
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
                var userId = UserSession.Instance.CurrentUser.Id;
                Orders = new ObservableCollection<Order>(
                    context.Orders
                        .Where(o => o.UserId == userId)
                        .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Comix)
                        .OrderByDescending(o => o.OrderDate)
                        .ToList()
                );
            }
        }
    }
}
