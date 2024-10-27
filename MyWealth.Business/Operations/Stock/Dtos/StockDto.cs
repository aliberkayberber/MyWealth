using MyWealth.Business.Operations.Comment.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Stock.Dtos
{
    public class StockDto
    {
        public int Id { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public decimal Purchase { get; set; }
        
        public decimal LastDiv { get; set; } // Divdend

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }

        public List<StockCommentDto> StockComments { get; set; }
    }
}
