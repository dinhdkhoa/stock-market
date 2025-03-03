using stock_market.Dtos.Comment;
using stock_market.Helplers;
using stock_market.Models;

namespace stock_market.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetCommentsAsync(CommentQueryObject queryObject);
    Task<Comment?> GetCommentById(int id);
    Task<Comment> CreateComment(Comment comment);
    Task<Comment?> UpdateComment(UpdateCommentDto comment, int id);
    Task<Comment?> Delete(int id);

}