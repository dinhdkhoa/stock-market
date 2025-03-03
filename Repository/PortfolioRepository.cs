using Microsoft.EntityFrameworkCore;
using stock_market.Data;
using stock_market.Dtos.Comment;
using stock_market.Dtos.Stock;
using stock_market.Interfaces;
using stock_market.Models;

namespace stock_market.Repository;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly AppDbContext _context;

    public PortfolioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Stock?> AddStock(Stock stock, AppUser user)
    {
        var portfolio = new Portfolio
        {
            AppUserId = user.Id,
            StockId = stock.Id
        };
        await _context.Portfolios.AddAsync(portfolio);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Portfolio> DeleteStock(string symbol, AppUser user)
    {
        var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == user.Id
        && x.Stock.Symbol.ToLower() == symbol.ToLower());
        if (portfolioModel == null) return null;
        _context.Portfolios.Remove(portfolioModel);
        await _context.SaveChangesAsync();
        return portfolioModel;
    }

    public async Task<List<PortfolioDto>> GetPorfolios(AppUser user)
    {
        return await _context.Portfolios.Where(u => u.AppUserId == user.Id).Select(p => new PortfolioDto()
        {
            Id = p.StockId,
            Symbol = p.Stock.Symbol,
            CompanyName = p.Stock.CompanyName,
            Purchase = p.Stock.Purchase,
            LastDiv = p.Stock.LastDiv,
            Industry = p.Stock.Industry,
            MarketCap = p.Stock.MarketCap
        }).ToListAsync();

    }
}