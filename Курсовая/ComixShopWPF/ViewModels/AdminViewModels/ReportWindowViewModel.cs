using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComixShopWPF.Commands.@base;
using ComixShopWPF.Data;
using System.Windows.Input;
using ComixShopWPF.ViewModels.Base;
using ComixShopWPF.Models;
using Microsoft.Win32;
using System.IO;

namespace ComixShopWPF.ViewModels.AdminViewModels
{
    public class ReportWindowViewModel:ViewModel
    {
        public ObservableCollection<Order> Orders { get; set; } = new();
        public ObservableCollection<SalesReportItem> ReportItems { get; set; } = new();

        public DateTime StartDate { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime EndDate { get; set; } = DateTime.Now;

        public ICommand GenerateReportCommand { get; }
        public ICommand ExportReportCommand { get; }

        public ReportWindowViewModel()
        {
            LoadOrders();
            GenerateReportCommand = new RelayCommand(GenerateReport);
            ExportReportCommand = new RelayCommand(ExportReport);
        }

        private void LoadOrders()
        {
            using var context = new DataContext();
            Orders = new ObservableCollection<Order>(
                context.Orders.Where(order => order.OrderDate >= StartDate && order.OrderDate <= EndDate).ToList()
            );
        }

        private void GenerateReport(object parameter)
        {
            ReportItems.Clear();

            var groupedData = Orders.SelectMany(o => o.OrderItems)
                .GroupBy(oi => oi.Comix.Title)
                .Select(g => new SalesReportItem
                {
                    Title = g.Key,
                    TotalQuantity = g.Sum(oi => oi.Quantity),
                    TotalRevenue = g.Sum(oi => oi.Quantity * oi.PriceAtPurchase)
                });

            foreach (var item in groupedData)
            {
                ReportItems.Add(item);
            }
        }

        private void ExportReport(object parameter)
        {
            SaveFileDialog dialog = new()
            {
                Filter = "CSV Files|*.csv",
                FileName = $"SalesReport_{DateTime.Now:yyyyMMdd}.csv"
            };
            if (dialog.ShowDialog() == true)
            {
                ExportToCSV(dialog.FileName);
            }
        }

        private void ExportToCSV(string filePath)
        {
            var lines = new List<string>
            {
                "Title,Total Quantity,Total Revenue"
            };
            lines.AddRange(ReportItems.Select(item => $"{item.Title},{item.TotalQuantity},{item.TotalRevenue:0.00}"));

            System.IO.File.WriteAllLines(filePath, lines);
        }
    }
    public class SalesReportItem
    {
        public string Title { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
