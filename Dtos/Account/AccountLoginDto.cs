using System.ComponentModel.DataAnnotations;

namespace stock_market.Dtos.Account;

public class AccountLoginDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}