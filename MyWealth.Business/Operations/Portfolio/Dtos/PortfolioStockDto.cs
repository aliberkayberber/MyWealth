using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Portfolio.Dtos
{
    public class PortfolioStockDto
    {
        public int Id {  get; set; }
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public decimal Purchase {  get; set; }
        public string Industry { get; set; }
        public long MarketCap {  get; set; }
        public decimal LastDiv { get; set; }
    }
}
