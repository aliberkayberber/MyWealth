using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Portfolio.Dtos
{
    public class UpdatePortfolioDto
    {  
        public string Username { get; set; }
      
        public List<int> StockIds { get; set; }
    }
}
