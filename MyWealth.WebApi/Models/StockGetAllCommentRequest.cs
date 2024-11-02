using System.ComponentModel.DataAnnotations;

namespace MyWealth.WebApi.Models
{
    public class StockGetAllCommentRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int PageNumber { get; set; } = 1;
        [Required]
        public int PageSize { get; set; } = 10;
    }
}
