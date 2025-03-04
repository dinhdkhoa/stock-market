using Microsoft.EntityFrameworkCore;

namespace stock_market.Models;

public class RefreshToken
{
    public int Id { get; set; }
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public string Token { get; set; }
}