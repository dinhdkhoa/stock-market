using System.ComponentModel.DataAnnotations;

namespace stock_market.Dtos.Comment;

public class UpdateCommentDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Title must be 5 characters")]
    [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MinLength(5, ErrorMessage = "Content must be 5 characters")]
    [MaxLength(280, ErrorMessage = "Content cannot be over 280 characters")]
    public string Content { get; set; } = string.Empty;
}