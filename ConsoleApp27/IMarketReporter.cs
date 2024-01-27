using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    internal interface IMarketReporter {
        double GetAllProfit();
        double GetAlcoholProfit();
        double GetNonAlcoholProfit();
    }
}
