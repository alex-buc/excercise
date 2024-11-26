using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RankController : ControllerBase 
{
    private readonly MyDbContext _myDdContext;

    public RankController(MyDbContext myDdContext)
    {
        _myDdContext = myDdContext;
    }

    [HttpGet("ranks")]
    public async Task<IActionResult> getAllRanks()
    {
        var allRanks = await _myDdContext.Ranks.ToListAsync();
        return Ok(allRanks);
    }

    [HttpPost("add")]
    public async Task<IActionResult> addRank([FromQueryAttribute] string rankName )
    {
        if (string.IsNullOrEmpty(rankName))
        {
            return BadRequest("Invalid rank name.");
        }

        RankViewModel model = new RankViewModel {
            RankName = rankName
        };

        _myDdContext.Ranks.Add(model);
        await _myDdContext.SaveChangesAsync();

        return CreatedAtAction(nameof(getAllRanks), new { id = model.Id }, model);
    }
}