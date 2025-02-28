using Microsoft.AspNetCore.Identity;

namespace stock_market.Models;

public class AppUser : IdentityUser
{
    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

}