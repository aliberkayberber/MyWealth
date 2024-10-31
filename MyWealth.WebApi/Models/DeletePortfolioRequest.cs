using System.ComponentModel.DataAnnotations;

namespace MyWealth.WebApi.Models
{
    public class DeletePortfolioRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Symbol { get; set; }
    }
}
