using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    internal class DrinkProduct : Product {

        public double AlcoholPercent;

        public DrinkProduct(string name, double salePrice, double costPrice, DateTime expireDate, double alcoholPercent) 
            : base(name, salePrice, costPrice, expireDate) {
            AlcoholPercent = alcoholPercent;
        }

        public override string ToString() {
            return base.ToString() + $"AlcoholPercent: {AlcoholPercent}\n";
        }
    }
}