using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComixShopWPF.Data;
using System.Windows;
using ComixShopWPF.Models;
using ComixShopWPF.ViewModels.Base;
using ComixShopWPF.Views.Frames;
using ComixShopWPF.Views.Windows;
using Microsoft.EntityFrameworkCore;
using ComixShopWPF.Commands.@base;
using System.Windows.Input;
using System.Windows.Controls;
using System.DirectoryServices;
using System.Text.RegularExpressions;
using ComixShopWPF.Services;

namespace ComixShopWPF.ViewModels
{
    public class CatalogViewModel:ViewModel
    {
        private ObservableCollection<Comix> comixList;
        public ObservableCollection<Comix> ComixList{get => comixList; set { Set(ref comixList, value); } }
        private string? selectedGenre;
        public string? SelectedGenre{get => selectedGenre; set{Set(ref selectedGenre, value);} }
        public List<string> FilterText { get; set; } = ["", "Стрип", "Графические романы", "Манхва"];
        public ObservableCollection<Comix> SearchResult = [];
        public ObservableCollection<Comix> Comixes = [];
        public ICommand SearchComixCommand { get; set; }
        public ICommand ResetComixCommand { get; set; }
        public ICommand ComixSelectedCommand { get; set; }
        private string searchBox;
        public string SearchBox { get => searchBox; set { Set(ref searchBox, value); } }
        private string priceDiapLeft;
        public string PriceDiapLeft { get => priceDiapLeft; set { Set(ref priceDiapLeft, value); } }
        private string priceDiapRight;
        public string PriceDiapRight { get => priceDiapRight; set { Set(ref priceDiapRight, value); } }


        private Comix selectedComix;
        public Comix SelectedComix { get => selectedComix; set { Set(ref selectedComix, value); } }
        public ICommand AddToCartCommand { get; set; }
        public ICommand AddMultipleToCartCommand { get; set; }

        public CatalogViewModel()
        {
            using (var context = new DataContext())
            {
                Comixes = new ObservableCollection<Comix>(context.Comixes.ToList());
                ComixList = Comixes;
            }
            SearchComixCommand = new RelayCommand(SearchComix_Executed);
            ResetComixCommand = new RelayCommand(ResetComix_Executed);
            ComixSelectedCommand = new RelayCommand(ComixSelected_Executed);
            AddToCartCommand = new RelayCommand(AddToCart, CanAddToCart);
            AddMultipleToCartCommand = new RelayCommand(AddMultipleToCart, CanAddToCart);

        }

        private bool CanAddToCart(object? parameter)
        {
            return true;
            //return SelectedComix != null;
        }
        private void AddToCart(object? parameter)
        {
            if (SelectedComix == null)
            {
                MessageBox.Show("Не выбран товар");
                return;
            }

            UserSession.Instance.AddToCart(SelectedComix, 1);
        }
        private int GetQuantityFromUser()
        {
            var input = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите количество товара для добавления в корзину:",
                "Добавить товар",
                "1"
            );

            if (int.TryParse(input, out int quantity) && quantity > 0)
            {
                return quantity;
            }

            MessageBox.Show("Неправильно");
            return 0;
        }
        private void AddMultipleToCart(object? parameter)
        {
            if (SelectedComix == null)
            {
                MessageBox.Show("Не выбран товар этофывфдыв фдыжвлфыждвлфыждвлфыджвфлы");
                return;
            }

            int quantity = GetQuantityFromUser();
            if (quantity > 0)
            {
                UserSession.Instance.AddToCart(SelectedComix, quantity);
            }
        }
        public void ComixSelected_Executed(object? comix)
        {
            if (comix != null)
            {
                Comix comix1 = (Comix)comix;
                Window cmxWindow = new ComixOverviewWindow(comix1);
                cmxWindow.Show();
                //MessageBox.Show($"{comix1.Title}");
            }
        }
        public void SearchComix_Executed(object? sender)
        {
            SearchResult.Clear();

            decimal price1 = ParsePrice(PriceDiapLeft, 0);
            decimal price2 = ParsePrice(PriceDiapRight, decimal.MaxValue);

            decimal priceLeft = Math.Min(price1, price2);
            decimal priceRight = Math.Max(price1, price2);

            bool hasSearchText = !string.IsNullOrWhiteSpace(SearchBox);
            bool hasGenre = !string.IsNullOrWhiteSpace(SelectedGenre);

            foreach (var c in Comixes)
            {
                bool matchesText = false;
                bool matchesGenre = false;
                bool matchesPrice = c.Price >= priceLeft && c.Price <= priceRight;

                if (hasSearchText)
                {
                    if (Regex.IsMatch(c.Title, SearchBox, RegexOptions.IgnoreCase) ||
                        Regex.IsMatch(c.Description, SearchBox, RegexOptions.IgnoreCase) ||
                        Regex.IsMatch(c.Author.ToLower(), SearchBox, RegexOptions.IgnoreCase))
                    {
                        matchesText = true;
                    }
                }
                else
                {
                    matchesText = true;
                }

                if (hasGenre)
                {
                    if (c.Genre == SelectedGenre)
                    {
                        matchesGenre = true;
                    }
                }
                else
                {
                    matchesGenre = true;
                }

                if (matchesText && matchesGenre && matchesPrice)
                {
                    SearchResult.Add(c);
                }
            }

            if (SearchResult.Any())
            {
                ComixList = SearchResult;
                //var resultWindow = new SearchWindow(SearchResult);
                //resultWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ничего не найдено");
            }
        }

        private decimal ParsePrice(string priceInput, decimal defaultValue)
        {
            if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out var price))
            {
                return price;
            }
            return defaultValue;
        }



        private void ResetComix_Executed(object? parm)
        {
            SearchBox = "";
            PriceDiapLeft = "";
            PriceDiapRight = "";
            SelectedGenre = "";
            ComixList = Comixes;
        }
    }
}


//public void PriceDiap_Executed(object sender, ExecutedRoutedEventArgs e)
//{
//    SearchResult.Clear();

//    int minPrice;
//    int maxPrice;

//    if (Convert.ToInt32(FromPrice.Text) < Convert.ToInt32(ToPrice.Text))
//    {
//        minPrice = Convert.ToInt32(FromPrice.Text);
//        maxPrice = Convert.ToInt32(ToPrice.Text);
//    }
//    else
//    {
//        minPrice = Convert.ToInt32(ToPrice.Text);
//        maxPrice = Convert.ToInt32(FromPrice.Text);
//    }
//    foreach (var a in ComixModelList)
//    {
//        if ((a.Price >= minPrice) && (a.Price <= maxPrice))
//        {
//            SearchResult.Add(a);
//        }
//    }
//    var Result = new SearchWindow(SearchResult);
//    Result.ShowDialog();
//}


//public void PriceDiap_CanExecute(object sender, CanExecuteRoutedEventArgs e)
//{
//    if ((int.TryParse(FromPrice.Text, out int result)) && (int.TryParse(ToPrice.Text, out int result1)))
//    {
//        e.CanExecute = true;
//        return;
//    }
//    e.CanExecute = false;
//}



//public bool IsSearchEnabled =>
//        !string.IsNullOrEmpty(SearchText) &&
//        (string.IsNullOrEmpty(MinPriceString) || decimal.TryParse(MinPriceString, out _)) &&
//        (string.IsNullOrEmpty(MaxPriceString) || decimal.TryParse(MaxPriceString, out _));
