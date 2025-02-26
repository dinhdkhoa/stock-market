using Microsoft.AspNetCore.Mvc;
using stock_market.Dtos.Stock;
using stock_market.Interfaces;
using stock_market.Mappers;

namespace stock_market.Controllers;

[Route("stocks")]
[ApiController]
public class StockController: ControllerBase
{
    private readonly IStockRepository _repo;

    public StockController( IStockRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    // [Route("")]
    public async Task<IActionResult> GetStocks()
    {
        var stocksFromDb = await _repo.GetStocksAsync();
        var stocks = stocksFromDb.Select(
            s => s.ToStockDto());
        return Ok(stocks);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStockById([FromRoute] int id)
    {
        var stock = await _repo.GetStockById(id);
        if(stock == null) return NotFound();
        return Ok(stock.ToStockDto());
    }
    [HttpPost]
    public async Task<IActionResult> Add(StockPostReqDto req)
    {
        var newStock = req.StockPostReqToStock();
        await _repo.Create(newStock);
        return CreatedAtAction(nameof(GetStockById), new {id = newStock.Id}, newStock.ToStockDto());
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stock = await _repo.Delete(id);
        if(stock == null) return NotFound();
        return NoContent();
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StockUpdateReqDto req)
    
    {
        var stock = await _repo.Update(id, req);
        if(stock == null) return NotFound();
        return Ok(stock.ToStockDto());
    }
}