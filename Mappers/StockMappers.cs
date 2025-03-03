using stock_market.Dtos.Stock;
using stock_market.Models;

namespace stock_market.Mappers;

public static class StockMappers
{
    public static StockDto ToStockDto(this Stock stock)
    {
        return new StockDto
        {
            Id = stock.Id,
            Symbol = stock.Symbol,
            Industry = stock.Industry,
            CompanyName = stock.CompanyName,
            Purchase = stock.Purchase,
            LastDiv = stock.LastDiv,
            MarketCap = stock.MarketCap,
            Comments = stock.Comments.Select(c => c.ToCommentDto()).ToList()
        };
    }
    
    public static Stock ToStockFromFmp(this FmpStock fmp)
    {
        var a = fmp;
        return new Stock
        {
            Symbol = fmp.symbol,
            Industry = fmp.industry,
            CompanyName = fmp.companyName,
            Purchase = (decimal)fmp.price,
            LastDiv = (decimal)fmp.lastDiv,
            MarketCap = fmp.mktCap ?? 0,
        };
    }
    
    public static Stock StockPostReqToStock(this StockPostReqDto req)
    {
        return new Stock
        {
            Symbol = req.Symbol,
            Industry = req.Industry,
            CompanyName = req.CompanyName,
            Purchase = req.Purchase,
            LastDiv = req.LastDiv,
            MarketCap = req.MarketCap
        };
    }
}