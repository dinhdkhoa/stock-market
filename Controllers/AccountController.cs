using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using stock_market.Dtos.Account;
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

    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signinManager = signInManager;

    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AccountRegisterDto registerDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        try
        {
            var appUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };
            
            //Zero7-Gently9-Crested2-Unequal8-Fruit8
            var result = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (!result.Succeeded) return StatusCode(500, result.Errors);
            
            var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
            if (!roleResult.Succeeded) return StatusCode(500, roleResult.Errors);

            return Ok(
                new NewUserDto
                {
                    UserName = appUser.UserName,
                    Email = appUser.Email,
                    Token = _tokenService.CreateToken(appUser)
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

        return Ok(
            new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            }
        );
    }
}
