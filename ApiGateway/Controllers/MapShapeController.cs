using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MapShapesController : ControllerBase 
{
    private readonly MyDbContext _myDdContext;

    public MapShapesController(MyDbContext myDdContext)
    {
        _myDdContext = myDdContext;
    }

    [HttpGet("")]
    public async Task<IActionResult> allMapShapes()
    {
        var allMapShapes = await _myDdContext.MapShapes.ToListAsync();
        return Ok(allMapShapes);
    }

    [HttpPost("")]
    public async Task<IActionResult> addMapShape(
        [FromQueryAttribute] string type, 
        [FromQueryAttribute] string data,
        [FromQueryAttribute] int missionId,
        [FromQueryAttribute] int? id
    )
    {
        MissionViewModel missionModel = _myDdContext.Missions.Find(missionId);
        if(missionModel == null) {
            return BadRequest("Invalid mission id.");
        }

        if(id == null) {
            MapShapeViewModel model = new MapShapeViewModel {
                Type = type,
                Data = data,
                Mission = missionModel
            };

            _myDdContext.MapShapes.Add(model);
            await _myDdContext.SaveChangesAsync();

            return CreatedAtAction(nameof(allMapShapes), new { id = model.Id }, model);
        }
        else {
           var existingShape = _myDdContext.MapShapes.Find(id.Value);
           if(existingShape == null) {
                return BadRequest("Invalid map shape id.");
           }
           existingShape.Data = data;
           _myDdContext.SaveChanges();
           return NoContent();
        }
    }
}