using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    internal class Product {

        public Product(string name, double salePrice, double costPrice, DateTime expireDate) {
            _staticNo++;
            No = _staticNo;
            Name = name;
            SalePrice = salePrice;
            CostPrice = costPrice;
            ExpireDate = expireDate;
        }

        static int _staticNo;
        public readonly int No;
        public string Name;
        public double SalePrice;
        public double CostPrice;
        public DateTime ExpireDate;

        public override string ToString() {
            return $"No: {No}\nName: {Name}\nPrice: {SalePrice}\nExpireDate: {ExpireDate.ToString("dd.MM.yyyy")}\n";
        }
    }
}