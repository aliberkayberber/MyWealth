using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Portfolio.Dtos
{
    public class PatchPortfolioDto
    {
        
        public string Username { get; set; }
        
        public string ChangeToSymbol { get; set; }
        public string Changing { get; set; }
    }
}
