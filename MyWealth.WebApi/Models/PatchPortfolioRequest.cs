using System.ComponentModel.DataAnnotations;

namespace MyWealth.WebApi.Models
{
    public class PatchPortfolioRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string ChangeToSymbol { get; set; }
        [Required]
        public string Changing {  get; set; }
    }
}
