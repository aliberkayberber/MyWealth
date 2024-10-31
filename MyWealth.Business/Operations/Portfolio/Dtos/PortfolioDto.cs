using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Portfolio.Dtos
{
    public class PortfolioDto
    {
        public string Username { get; set; }
        public List<PortfolioStockDto> Stocks { get; set; } = new List<PortfolioStockDto>();
    }
}
