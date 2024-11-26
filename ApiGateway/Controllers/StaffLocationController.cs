using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StaffLocationController : ControllerBase 
{
    private readonly MyDbContext _myDdContext;

    public StaffLocationController(MyDbContext myDdContext)
    {
        _myDdContext = myDdContext;
    }

    [HttpGet("mapShapes")]
    public async Task<IActionResult> allMapShapes()
    {
        var allMapShapes = await _myDdContext.MapShapes.ToListAsync();
        return Ok(allMapShapes);
    }

    [HttpPost("add")]
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