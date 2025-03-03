using stock_market.Models;

namespace stock_market.Interfaces;

public interface IFMPService
{
    Task<FMPStock?> GetFMPStock(string symbol);

}