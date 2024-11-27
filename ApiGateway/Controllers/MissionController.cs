using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MissionsController : ControllerBase 
{
    private readonly MyDbContext _myDdContext;

    public MissionsController(MyDbContext myDdContext)
    {
        _myDdContext = myDdContext;
    }

    [HttpGet("")]
    public async Task<IActionResult> getAllMissions()
    {
        var allMissions = await _myDdContext.Missions.ToListAsync();
        return Ok(allMissions);
    }

    [HttpPost("")]
    public async Task<IActionResult> addMission(
        [FromQueryAttribute] string fullName, 
        [FromQueryAttribute]  double startPositionLat, 
        [FromQueryAttribute] double startPositionLng
    )
    {
        if (string.IsNullOrEmpty(fullName))
        {
            return BadRequest("Invalid full name.");
        }

        MissionViewModel model = new MissionViewModel {
            FullName = fullName,
            StartPositionLat = startPositionLat,
            StartPositionLng = startPositionLng
        };

        _myDdContext.Missions.Add(model);
        await _myDdContext.SaveChangesAsync();

        return CreatedAtAction(nameof(getAllMissions), new { id = model.Id }, model);
    }
}