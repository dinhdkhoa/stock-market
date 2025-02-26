using Microsoft.EntityFrameworkCore;
using stock_market.Data;
using stock_market.Interfaces;
using stock_market.Models;

namespace stock_market.Repository;

public class StockRepository : IStockRepository
{
    private readonly AppDbContext _context;
    public StockRepository(AppDbContext context)
    {
        _context = context;

    }
    
    public async Task<List<Stock>> GetStock()
    {
        var stocks = await _context.Stocks.ToListAsync();
        return stocks;
    }
}