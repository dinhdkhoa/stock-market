using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using stock_market.Models;

namespace stock_market.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user, CreateTokenParams options);
    string CreateAccessToken(AppUser user);
    string CreateRefreshToken(AppUser user, DateTime? expires = null);
    
    bool ValidateRefreshToken(
        string token,
        out JwtSecurityToken jwt
    );
}

public class CreateTokenParams
{
    public SymmetricSecurityKey key { get; set; }
    public DateTime expires { get; set; }
}