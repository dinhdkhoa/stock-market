using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using stock_market.Data;
using stock_market.Dtos.Stock;
using stock_market.Interfaces;
using stock_market.Mappers;
using stock_market.Models;

namespace stock_market.Controllers;

[Route("stocks")]
[ApiController]
public class StockController: ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IStockRepository _repo;

    public StockController(AppDbContext context, IStockRepository repo)
    {
        _context = context;
        _repo = repo;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetStocks()
    {
        var stocksFromDb = await _repo.GetStock();
        var stocks = stocksFromDb.Select(
            s => s.ToStockDto());
        return Ok(stocks);
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetStockById([FromRoute] int id)
    {
        var stock = _context.Stocks.Find(id);
        if(stock == null) return NotFound();
        return Ok(stock.ToStockDto());
    }
    [HttpPost]
    public IActionResult Add(StockPostReqDto req)
    {
        var newStock = req.StockPostReqToStock();
        _context.Stocks.Add(newStock);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetStockById), new {id = newStock.Id}, newStock.ToStockDto());
    }
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);
        if(stock == null) return NotFound();
        _context.Stocks.Remove(stock);
        _context.SaveChanges();
        return NoContent();
    }
    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] StockUpdateReqDto req)
    
    {
        var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);
        if(stock == null) return NotFound();
        // _context.Entry(req).CurrentValues.SetValues(stock);
        stock.Symbol = req.Symbol;
        stock.Industry = req.Industry;
        stock.Purchase = req.Purchase;
        stock.LastDiv = req.LastDiv;
        stock.MarketCap = req.MarketCap;
        stock.CompanyName = req.CompanyName;
        
        _context.SaveChanges();
        return Ok(stock.ToStockDto());
    }
}