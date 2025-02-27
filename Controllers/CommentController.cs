using Microsoft.AspNetCore.Mvc;
using stock_market.Dtos.Comment;
using stock_market.Interfaces;
using stock_market.Mappers;

namespace stock_market.Controllers;

[Route("comments")]
[ApiController]
public class CommentsController: ControllerBase
{
    private readonly ICommentRepository _repo;
    private readonly IStockRepository _stockRepo;

    public CommentsController(ICommentRepository repo, IStockRepository stockRepo)
    {
        _repo = repo;
        _stockRepo = stockRepo;
    }

    [HttpGet]
    // [Route("")]
    public async Task<IActionResult> GetComments()
    {
        var commentsFromDb = await _repo.GetCommentsAsync();
        var comments = commentsFromDb.Select(
            s => s.ToCommentDto());
        return Ok(comments);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCommentById([FromRoute] int id)
    {
        var comment = await _repo.GetCommentById(id);
        if(comment == null) return NotFound("Comment does not exists");
        return Ok(comment.ToCommentDto());
    }
    
    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> Add(CreateCommentDto req, int stockId)
    {
        if (!await _stockRepo.StockIdExists(stockId)) return BadRequest("Comment does not exists");
        var comment = req.ToCommentFromCreate(stockId);
        await _repo.CreateComment(comment);
        return CreatedAtAction(nameof(GetCommentById), new {id = comment.Id}, comment.ToCommentDto());
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var comment = await _repo.Delete(id);
        if(comment == null) return NotFound("Comment does not exists");
        return NoContent();
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto req)
    
    {
        var comment = await _repo.UpdateComment(req , id);
        if(comment == null) return NotFound("Comment does not exists");
        return Ok(comment.ToCommentDto());
    }
}