using System.Text.Json.Serialization;

namespace stock_market.Dtos.Account;

public class NewUserDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string UserName { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Email { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

}