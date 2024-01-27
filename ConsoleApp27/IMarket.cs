using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    internal interface IMarket {
        Product[] Products { get; }
        void AddProduct(Product pr);
        void RemoveProductByNo(int no);
    }
}
