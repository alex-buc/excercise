using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StaffsController : ControllerBase 
{
    private readonly MyDbContext _myDdContext;

    public StaffsController(MyDbContext myDdContext)
    {
        _myDdContext = myDdContext;
    }

    [HttpGet("")]
    public async Task<IActionResult> getAllStaffs()
    {
        var allStaffs = await _myDdContext.Staffs.ToListAsync();
        return Ok(allStaffs);
    }

    [HttpGet("{staffId}/missions")]
    public async Task<IActionResult> getAllMissionsByStuffId(int staffId) {
        StaffViewModel userModel = _myDdContext.Staffs.Find(staffId);
        if(userModel == null) {
            return BadRequest("Invalid staff id.");
        }

        List<MissionViewModel> missions = await _myDdContext.MissionAllocations
            .Where(ma => ma.Staff.Id == staffId)
            .Include(ma => ma.Mission)
            .Select(ma => ma.Mission)
            .ToListAsync();
            
        return Ok(missions);
    }

    [HttpPost("")]
    public async Task<IActionResult> addStaff(
        [FromQueryAttribute] string fullName, 
        [FromQueryAttribute] string identificationColor, 
        [FromQueryAttribute] string initials, 
        [FromQueryAttribute] int userId, 
        [FromQueryAttribute] int rankId, 
        [FromQueryAttribute] int? supervisorId
    )
    {
        UserViewModel userModel = _myDdContext.Users.Find(userId);
        if(userModel == null) {
            return BadRequest("Invalid user id.");
        }

        RankViewModel rankModel = _myDdContext.Ranks.Find(rankId);
        if(rankModel == null) {
            return BadRequest("Invalid rank id.");
        }

        StaffViewModel superiorModel = null;
        if(supervisorId != null) {
            superiorModel = _myDdContext.Staffs.Find(supervisorId);
            if(superiorModel == null) {
                return BadRequest("Invalid supervisor id.");
            }
        }

        StaffViewModel model = new StaffViewModel {
            FullName = fullName,
            IdentificationColor = identificationColor,
            Initials = initials,
            User = userModel,
            Rank = rankModel,
            Superior = superiorModel
        };
        
        _myDdContext.Staffs.Add(model);

        await _myDdContext.SaveChangesAsync();

        return CreatedAtAction(nameof(getAllStaffs), new { id = model.Id }, model);
    }
}