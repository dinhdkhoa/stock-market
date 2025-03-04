using Microsoft.EntityFrameworkCore;
using stock_market.Data;
using stock_market.Interfaces;
using stock_market.Models;

namespace stock_market.Repository;

public class RefreshTokenRepository :  IRefreshTokenRepository

{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;

    }

    public async Task<RefreshToken?> DeleteToken(string Token, AppUser user)
    {
        var token = await _context.RefreshToken.FirstOrDefaultAsync(x => x.Token == Token && x.AppUserId == user.Id);
        if (token == null) return token;
        _context.RefreshToken.Remove(token);
        await _context.SaveChangesAsync();
        return token;
    }

    public async Task<RefreshToken> AddToken(string Token, AppUser user)
    {
        var refreshToken = new RefreshToken
        {
            Token = Token,
            AppUserId = user.Id,
        };
        await _context.RefreshToken.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
        return refreshToken;
    }
}