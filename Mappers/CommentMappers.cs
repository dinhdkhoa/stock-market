using stock_market.Dtos.Comment;
using stock_market.Models;

namespace stock_market.Mappers;

public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            Title = comment.Title,
            CreatedOn = comment.CreatedOn,
            StockId = comment.StockId,
            
        };
    }
    public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockId)
    {
        return new Comment
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
            StockId = stockId
        };
    }
}