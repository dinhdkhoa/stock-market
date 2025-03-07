using Microsoft.EntityFrameworkCore;
using stock_market.Data;
using stock_market.Dtos.Stock;
using stock_market.Helplers;
using stock_market.Interfaces;
using stock_market.Mappers;
using stock_market.Models;
using stock_market.Service;

namespace stock_market.Repository;

public class StockRepository : IStockRepository
{
    private readonly AppDbContext _context;
    private readonly FmpService _fmp;
    public StockRepository(AppDbContext context, FmpService fmp)
    {
        _context = context;
        _fmp = fmp;
    }
    
    public async Task<List<Stock>> GetStocksAsync(QueryObject query)
    {
        var stocks = _context.Stocks.Include(s => s.Comments).ThenInclude(c => c.AppUser).AsQueryable();
        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(x => x.CompanyName.ToLower().Contains(query.CompanyName.ToLower()));
        }
        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(x => x.Symbol.ToLower().Contains(query.Symbol.ToLower()));
        }
        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
            }
        }
        var skipNumber = (query.PageNumber - 1) * query.PageSize;


        return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<Stock?> GetStockById(int id)
    {
        var stock = await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
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
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if(stock == null) return null;
        _context.Stocks.Remove(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public Task<bool> StockIdExists(int id)
    {
        return _context.Stocks.AnyAsync(x => x.Id == id);
    }

    public async Task<Stock?> GetStockBySymbol(string symbol)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol.ToLower() == symbol.ToLower());
        if (stock == null)
        {
            var fmpStock  = await _fmp.GetFmpStock(symbol);
            stock = fmpStock.ToStockFromFmp();
            await Create(stock);
        }
        
        return stock;
    }
}