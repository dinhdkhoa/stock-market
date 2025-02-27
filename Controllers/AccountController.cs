using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using stock_market.Dtos.Account;
using stock_market.Models;

namespace stock_market.Controllers;

[Route("account")]
[ApiController]
public class AccountController: ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    public AccountController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
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
            return !roleResult.Succeeded ? StatusCode(500, result.Errors) : Ok("User created");
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }
}