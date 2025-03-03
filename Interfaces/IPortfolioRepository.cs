using stock_market.Dtos.Comment;
using stock_market.Dtos.Stock;
using stock_market.Models;

namespace stock_market.Interfaces;

public interface IPortfolioRepository
{
    Task<List<PortfolioDto>> GetPorfolios(AppUser user);

    Task<Stock?> AddStock(Stock stock, AppUser user);

    Task<Portfolio> DeleteStock(string symbol, AppUser user);

}