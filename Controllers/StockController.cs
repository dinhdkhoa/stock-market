using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using stock_market.Dtos.Stock;
using stock_market.Helplers;
using stock_market.Interfaces;
using stock_market.Mappers;
using stock_market.Service;

namespace stock_market.Controllers;

[Route("stocks")]
[ApiController]
public class StockController: ControllerBase
{
    private readonly IStockRepository _repo;
    private readonly IFMPService _fmpService;

    public StockController( IStockRepository repo, IFMPService fmpService)
    {
        _repo = repo;
        _fmpService = fmpService;
    }

    [HttpGet]
    // [Route("")]
    public async Task<IActionResult> GetStocks([FromQuery] QueryObject query)
    {
        var stocksFromDb = await _repo.GetStocksAsync(query);
        var stocks = stocksFromDb.Select(
            s => s.ToStockDto());
        return Ok(stocks);
    }
    [HttpGet]
    [Route("{symbol}")]
    public async Task<IActionResult> GetStocks([FromRoute] string symbol)
    {
        var stock = await _fmpService.GetFMPStock(symbol);
        if(stock == null) return NotFound();
        return Ok(stock);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStockById([FromRoute] int id)
    {
        var stock = await _repo.GetStockById(id);
        if(stock == null) return NotFound();
        return Ok(stock.ToStockDto());
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(StockPostReqDto req)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var stock = await _repo.Update(id, req);
        if(stock == null) return NotFound();
        return Ok(stock.ToStockDto());
    }
}