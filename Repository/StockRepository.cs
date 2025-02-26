using Microsoft.EntityFrameworkCore;
using stock_market.Data;
using stock_market.Dtos.Stock;
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

    public async Task<Stock?> GetStockById(int id)
    {
        var stock = await _context.Stocks.FindAsync(id);
        return stock;
    }

    public async Task<Stock?> Create(Stock stock)
    {
        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> Update(int id, StockUpdateReqDto req)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if(stock == null) return null;
        // _context.Entry(req).CurrentValues.SetValues(stock);

        stock.Symbol = req.Symbol;
        stock.Industry = req.Industry;
        stock.Purchase = req.Purchase;
        stock.LastDiv = req.LastDiv;
        stock.MarketCap = req.MarketCap;
        stock.CompanyName = req.CompanyName;
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> Delete(int id)
    {
        var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);
        if(stock == null) return null;
        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();
        return stock;
    }
}