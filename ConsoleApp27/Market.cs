using ConsoleApp27;
using StoreApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    internal class Market : IMarket, IMarketReporter {
        private Product[] _products = new Product[0];
        public Product[] Products => _products;
        public int AlcoholPercentLimit;

        public double AvgAlcoholPercent {
            get {
                DrinkProduct[] alcoholDrinks = GetAllAlcoholDrinks();

                double totalPercent = 0;
                for (int i = 0; i < alcoholDrinks.Length; i++)
                    totalPercent += alcoholDrinks[i].AlcoholPercent;

                return alcoholDrinks.Length == 0 ? 0 : totalPercent / alcoholDrinks.Length;
            }
        }

        public void AddProduct(Product product) {
            if (product is DrinkProduct drink && drink.AlcoholPercent > AlcoholPercentLimit)
                return;

            Array.Resize(ref _products, _products.Length + 1);
            _products[_products.Length - 1] = product;
        }

        public double GetAlcoholProfit() {
            double alcoholProfit = 0;
            foreach (var product in Products) {
                if (product is DrinkProduct drinkProduct && drinkProduct.AlcoholPercent > 0) {
                    alcoholProfit += (drinkProduct.SalePrice - drinkProduct.CostPrice);
                }
            }
            return alcoholProfit;
        }

        public double GetAllProfit() {
            double totalProfit = 0;
            foreach (var product in Products) {
                totalProfit += (product.SalePrice - product.CostPrice);
            }
            return totalProfit;
        }


        public double GetNonAlcoholProfit() {
            double nonAlcoholProfit = 0;
            foreach (var product in Products) {
                if (product is DrinkProduct drinkProduct && drinkProduct.AlcoholPercent == 0) {
                    nonAlcoholProfit += (drinkProduct.SalePrice - drinkProduct.CostPrice);
                }
            }
            return nonAlcoholProfit;
        }


        public void RemoveProductByNo(int no) {
            var wantedProduct = FindByNo(no);
            if (wantedProduct is null)
                throw new ProductNotFoundException();

            if (wantedProduct.ExpireDate >= DateTime.Now.AddYears(1))
                throw new ProductExpireDateException();

            var wantedIndex = FindIndexByNo(no);
            for (int i = wantedIndex; i < _products.Length - 1; i++) {
                Product temp = _products[i];
                _products[i] = _products[i + 1];
                _products[i + 1] = temp;
            }

            Array.Resize(ref _products, _products.Length - 1);

        }

        public Product? FindByNo(int no) {
            for (int i = 0; i < _products.Length; i++) {
                if (_products[i].No == no) {
                    return _products[i];
                }
            }
            return null;
        }

        public int FindIndexByNo(int no) {
            for (int i = 0; i < _products.Length; i++) {
                if (_products[i].No == no) {
                    return i;
                }
            }
            return -1;
        }

        public DrinkProduct[] GetAllAlcoholDrinks() {
            DrinkProduct[] drinks = new DrinkProduct[0];

            for (int i = 0; i < _products.Length; i++) {
                if (_products[i] is DrinkProduct drink && drink.AlcoholPercent > 0) {
                    Array.Resize(ref drinks, drinks.Length + 1);
                    drinks[drinks.Length - 1] = drink;
                }
            }
            return drinks;
        }

        public DrinkProduct[] GetAllDrinks() {
            DrinkProduct[] drinks = new DrinkProduct[0];

            for (int i = 0; i < _products.Length; i++) {
                if (_products[i] is DrinkProduct drink) {
                    Array.Resize(ref drinks, drinks.Length + 1);
                    drinks[drinks.Length - 1] = drink;
                }
            }

            return drinks;
        }

        public DrinkProduct[] GetAllNonAlcoholDrinks() {
            DrinkProduct[] drinks = new DrinkProduct[0];

            for (int i = 0; i < _products.Length; i++) {
                if (_products[i] is DrinkProduct drink && drink.AlcoholPercent == 0) {
                    Array.Resize(ref drinks, drinks.Length + 1);
                    drinks[drinks.Length - 1] = drink;
                }
            }
            return drinks;
        }

    }
}