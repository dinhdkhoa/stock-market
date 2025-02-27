using Microsoft.EntityFrameworkCore;
using stock_market.Data;
using stock_market.Dtos.Comment;
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

    public async Task<Comment> CreateComment(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> UpdateComment(UpdateCommentDto commentDto, int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null) return null;
        comment.Content = commentDto.Content;
        comment.Title = commentDto.Title;
        await _context.SaveChangesAsync();
        return comment;
    }
    
    public async Task<Comment?> Delete(int id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        if(comment == null) return null;
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return comment;
    }
}