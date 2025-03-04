using stock_market.Dtos.Comment;
using stock_market.Helplers;
using stock_market.Models;

namespace stock_market.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> DeleteToken(string Token,  AppUser user);
    Task<RefreshToken> AddToken(string Token,  AppUser user);
}