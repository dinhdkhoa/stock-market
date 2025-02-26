using stock_market.Models;

namespace stock_market.Interfaces;

public interface IStockRepository
{
    public Task<List<Stock>> GetStock();
}