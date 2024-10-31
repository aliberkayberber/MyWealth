using System.ComponentModel.DataAnnotations;

namespace MyWealth.WebApi.Models
{
    public class UpdatePortfolioRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public List<int> StockIds { get; set; }
    }
}
