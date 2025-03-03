using Newtonsoft.Json;
using stock_market.Interfaces;
using stock_market.Models;

namespace stock_market.Service;

public class FMPService : IFMPService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    public FMPService(HttpClient http,IConfiguration config)
    {
        _http = http;
        _config = config;
    }
    
    public async Task<FMPStock?> GetFMPStock(string symbol)
    {
        try
        {
            var response = _http
                .GetAsync(
                    $"https://financialmodelingprep.com/stable/search-exchange-variants?symbol={symbol}&apikey={_config["FMP_KEY"]}")
                .Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var stocks = JsonConvert.DeserializeObject<List<FMPStock>>(jsonData);

                return stocks?.FirstOrDefault(x => x.symbol == symbol);
            }
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}   