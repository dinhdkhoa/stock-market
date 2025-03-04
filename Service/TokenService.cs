using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using stock_market.Interfaces;
using stock_market.Models;

namespace stock_market.Service;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string CreateToken(AppUser user, CreateTokenParams options)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
        };

        var creds = new SigningCredentials(options.key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = options.expires,
            SigningCredentials = creds,
            Issuer = _config["JWT:Issuer"],
            Audience = _config["JWT:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string CreateAccessToken(AppUser user)
    {
        var tokenInfo = new CreateTokenParams
        {
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"])),
            expires = DateTime.Now.AddDays(7)
        };
        return CreateToken(user, tokenInfo);
    }

    public string CreateRefreshToken(AppUser user, DateTime? expiresTime = null)
    {
        if (!expiresTime.HasValue)
        {
            expiresTime = DateTime.Now.AddMonths(1);
        }
        var tokenInfo = new CreateTokenParams
        {
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKeyRF"])),
            expires = (DateTime)expiresTime
        };
       return CreateToken(user, tokenInfo);
    }
    
    public bool ValidateRefreshToken(
        string token,
        out JwtSecurityToken jwt
    )
    {
        // {
        //     "username": "string4",
        //     "email": "user4@example.com",
        //     "password": "String1!"
        // }
        var key = _config["JWT:SigningKey"];
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _config["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = _config["JWT:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKeyRF"]))
        };

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            jwt = (JwtSecurityToken)validatedToken;
    
            return true;
        } catch (SecurityTokenValidationException ex) {
            jwt = null;
            return false;
        }
    }
    
}