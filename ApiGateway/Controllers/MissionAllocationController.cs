using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MissionsAllocationController : ControllerBase 
{
    private readonly MyDbContext _myDdContext;

    public MissionsAllocationController(MyDbContext myDdContext)
    {
        _myDdContext = myDdContext;
    }

    [HttpGet("")]
    public async Task<IActionResult> getMissionAllocations()
    {
        var allMissionAllocations = await _myDdContext.MissionAllocations.ToListAsync();
        return Ok(allMissionAllocations);
    }

    [HttpPost("")]
    public async Task<IActionResult> addMissionAllocations(
        [FromQueryAttribute] int staffId, 
        [FromQueryAttribute] int missionId
    )
    {
        StaffViewModel staffModel = _myDdContext.Staffs.Find(staffId);
        if(staffModel == null) {
            return BadRequest("Invalid staff id.");
        }
        
        MissionViewModel missionModel = _myDdContext.Missions.Find(missionId);
        if(missionModel == null) {
            return BadRequest("Invalid mission id.");
        }

        MissionAllocationViewModel model = new MissionAllocationViewModel {
            Staff = staffModel,
            Mission = missionModel
        };
        _myDdContext.MissionAllocations.Add(model);
        await _myDdContext.SaveChangesAsync();

        return CreatedAtAction(nameof(getMissionAllocations), new { id = model.Id }, model);
    }
}