using Microsoft.EntityFrameworkCore;
using stock_market.Data;
using stock_market.Interfaces;
using stock_market.Models;

namespace stock_market.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;

    }

    public async Task<List<Comment>> GetCommentsAsync()
    {
        var comments = await _context.Comments.ToListAsync();
        return comments;
    }

    public async Task<Comment?> GetCommentById(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        return comment;
    }
}