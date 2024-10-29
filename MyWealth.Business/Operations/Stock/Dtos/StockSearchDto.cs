using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Stock.Dtos
{
    public class StockSearchDto
    {
        public string Symbol { get; set; }

        public string CompanyName { get; set; } 

        public decimal Purchase { get; set; }

        public decimal LastDiv { get; set; } // Divdend

        public string Industry { get; set; }

        public long MarketCap { get; set; } 

    }
}
