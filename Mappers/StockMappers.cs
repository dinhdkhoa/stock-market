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
            MarketCap = stock.MarketCap
        };
    }public static Stock StockPostReqToStock(this StockPostReqDto req)
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