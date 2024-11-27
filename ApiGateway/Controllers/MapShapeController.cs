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
        [FromQueryAttribute] int staffId
    )
    {
        StaffViewModel staffModel = _myDdContext.Staffs.Find(staffId);
        if(staffModel == null) {
            return BadRequest("Invalid staff id.");
        }

        MapShapeViewModel model = new MapShapeViewModel {
            Type = type,
            Data = data,
            Staff = staffModel
        };

        _myDdContext.MapShapes.Add(model);
        await _myDdContext.SaveChangesAsync();

        return CreatedAtAction(nameof(allMapShapes), new { id = model.Id }, model);
    }
}