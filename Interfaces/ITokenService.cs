using stock_market.Models;

namespace stock_market.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}