using stock_market.Dtos.Stock;
using stock_market.Models;

namespace stock_market.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetStock();
    Task<Stock?> GetStockById(int id);
    Task<Stock?> Create(Stock stock);
    Task<Stock?> Update(int id, StockUpdateReqDto req);
    Task<Stock?> Delete(int id);
}