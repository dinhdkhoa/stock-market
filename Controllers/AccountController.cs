using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using stock_market.Dtos.Account;
using stock_market.Helplers;
using stock_market.Interfaces;
using stock_market.Models;

namespace stock_market.Controllers;

[Route("account")]
[ApiController]
public class AccountController: ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUser> _signinManager;
    private readonly IRefreshTokenRepository _refreshTokenRepo;

    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager, IRefreshTokenRepository refreshTokenRepo)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signinManager = signInManager;
        _refreshTokenRepo = refreshTokenRepo;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AccountRegisterDto registerDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        try
        {
            var user = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };
            
            //Zero7-Gently9-Crested2-Unequal8-Fruit8
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return StatusCode(500, result.Errors);
            
            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            if (!roleResult.Succeeded) return StatusCode(500, roleResult.Errors);

            var newAccessToken = _tokenService.CreateAccessToken(user);
            var newRefreshToken = _tokenService.CreateRefreshToken(user);
            await _refreshTokenRepo.AddToken(newRefreshToken, user);

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                }
            );
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(AccountLoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

        if (user == null) return Unauthorized("Invalid username!");

        var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");
        
        var newAccessToken = _tokenService.CreateAccessToken(user);
        var newRefreshToken = _tokenService.CreateRefreshToken(user);
        await _refreshTokenRepo.AddToken(newRefreshToken, user);

        return Ok(
            new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            }
        );
    }
    
    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        JwtSecurityToken jwt = null;
        var isRefreshTokenValid = _tokenService.ValidateRefreshToken(refreshToken, out jwt);
        if(!isRefreshTokenValid) return Unauthorized("Invalid refresh token!");
        var claims = jwt.Claims.Select(c => new
        {
            c.Type,
            c.Value
        });
        var username = claims.FirstOrDefault(s => s.Type == "given_name").Value;
        var oldExpireTime = DateTimeHelpers.FromUnixTime(long.Parse(claims.FirstOrDefault(s => s.Type == "exp").Value));
        
        if(string.IsNullOrEmpty(username)) return Unauthorized("Invalid refresh token!");

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username.ToLower());

        if (user == null) return Unauthorized("User Not Found!");
        
        var isDeleteToken = await _refreshTokenRepo.DeleteToken(refreshToken, user);
        if(isDeleteToken == null) return Unauthorized("Token not found!");
        
        var newAccessToken = _tokenService.CreateAccessToken(user);
        var newRefreshToken = _tokenService.CreateRefreshToken(user, oldExpireTime);
        
        await _refreshTokenRepo.AddToken(newRefreshToken, user);
        
        return Ok(new
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
        });
    }
}
