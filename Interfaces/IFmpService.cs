using stock_market.Models;

namespace stock_market.Interfaces;

public interface IFmpService
{
    Task<FmpStock?> GetFmpStock(string symbol);

}