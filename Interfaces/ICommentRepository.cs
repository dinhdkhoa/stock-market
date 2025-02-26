using stock_market.Models;

namespace stock_market.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetCommentsAsync();
    Task<Comment?> GetCommentById(int id);

}