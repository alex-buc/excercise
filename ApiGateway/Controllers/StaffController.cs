using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StaffController : ControllerBase 
{
    private readonly MyDbContext _myDdContext;

    public StaffController(MyDbContext myDdContext)
    {
        _myDdContext = myDdContext;
    }

    [HttpGet("staffs")]
    public async Task<IActionResult> getAllStaffs()
    {
        var allStaffs = await _myDdContext.Staffs.ToListAsync();
        return Ok(allStaffs);
    }

    [HttpPost("add")]
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
            user = userModel,
            Rank = rankModel,
            Superior = superiorModel
        };
        
        _myDdContext.Staffs.Add(model);

        await _myDdContext.SaveChangesAsync();

        return CreatedAtAction(nameof(getAllStaffs), new { id = model.Id }, model);
    }
}