using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using stock_market.Data;

namespace stock_market.Controllers;

[Route("stocks")]
[ApiController]
public class StockController: ControllerBase
{
    private readonly AppDbContext _context;
    
    public StockController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("")]
    public IActionResult GetStocks()
    {
        var stocks = _context.Stocks.ToList();
        return Ok(stocks);
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetStockById([FromRoute] int id)
    {
        var stock = _context.Stocks.Find(id);
        if(stock == null) return NotFound();
        return Ok(stock);
    }
}