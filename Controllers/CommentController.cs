using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stock_market.Dtos.Comment;
using stock_market.Extensions;
using stock_market.Interfaces;
using stock_market.Mappers;
using stock_market.Models;

namespace stock_market.Controllers;

[Route("comments")]
[ApiController]
public class CommentsController: ControllerBase
{
    private readonly ICommentRepository _repo;
    private readonly IStockRepository _stockRepo;
    private readonly UserManager<AppUser> _userManager;

    public CommentsController(ICommentRepository repo, UserManager<AppUser> userManager, IStockRepository stockRepo)
    {
        _repo = repo;
        _stockRepo = stockRepo;
        _userManager = userManager;

    }

    [HttpGet]
    // [Route("")]
    public async Task<IActionResult> GetComments()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var commentsFromDb = await _repo.GetCommentsAsync();
        var comments = commentsFromDb.Select(
            s => s.ToCommentDto());
        return Ok(comments);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCommentById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var comment = await _repo.GetCommentById(id);
        if(comment == null) return NotFound("Comment does not exists");
        return Ok(comment.ToCommentDto());
    }
    
    [HttpPost("{stockId:int}")]
    [Authorize]
    public async Task<IActionResult> Add(CreateCommentDto req, int stockId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);

        if (!await _stockRepo.StockIdExists(stockId)) return BadRequest("Comment does not exists");
        var comment = req.ToCommentFromCreate(stockId);
        comment.AppUser = appUser;
        await _repo.CreateComment(comment);
        return CreatedAtAction(nameof(GetCommentById), new {id = comment.Id}, comment.ToCommentDto());
    }
    
    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var comment = await _repo.Delete(id);
        if(comment == null) return NotFound("Comment does not exists");
        return NoContent();
    }
    
    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto req)
    
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var comment = await _repo.UpdateComment(req , id);
        if(comment == null) return NotFound("Comment does not exists");
        return Ok(comment.ToCommentDto());
    }
}