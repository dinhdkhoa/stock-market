using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stock_market.Dtos.Portfolio;
using stock_market.Extensions;
using stock_market.Interfaces;
using stock_market.Models;
using stock_market.Dtos.Stock;
using stock_market.Mappers;

namespace stock_market.Controllers
{
    [Route("portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _repo;
        private readonly UserManager<AppUser> _userManager;

        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo, IPortfolioRepository repo)
        {
            _stockRepo = stockRepo;
            _userManager = userManager;
            _repo = repo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPortfolios()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stocks = await _repo.GetPorfolios(appUser);

            return Ok(stocks);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio([FromBody] AddPortolioReqDto req)
        {
            var stock = await _stockRepo.GetStockBySymbol(req.Symbol);
            if (stock == null) return NotFound("Stock doesn\'t exist");
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var stocks = await _repo.GetPorfolios(appUser);

            if (stocks.Any(s => s.Id == stock.Id)) return BadRequest("Stock is already in portfolio");

            await _repo.AddStock(stock, appUser);

            return Ok(stock.ToStockDto());
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var stocks = await _repo.GetPorfolios(appUser);

            var filteredStock = stocks.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredStock.Count() == 0) return BadRequest("Stock isn\'t in portfolio");

            var portfolioModel = await _repo.DeleteStock(symbol, appUser);

            return Ok();
        }
    }  
}
