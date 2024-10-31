using System.ComponentModel.DataAnnotations;

namespace MyWealth.WebApi.Models
{
    public class AddCommentRequest
    {
        //public int Id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters")]
        [MaxLength(380, ErrorMessage = "Content cannot be over 280 characters")]
        public string Content { get; set; } = string.Empty;
        [Required]
        public int StockId { get; set; }
    }
}
