using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase 
{
    private readonly MyDbContext _myDdContext;

    public UserController(MyDbContext myDdContext)
    {
        _myDdContext = myDdContext;
    }

    [HttpGet("users")]
    public async Task<IActionResult> getAllUsers()
    {
        var allUsers = await _myDdContext.Users.ToListAsync();
        return Ok(allUsers);
    }

    [HttpPost("add")]
    public async Task<IActionResult> addUser(
        [FromQueryAttribute] string userName, 
        [FromQueryAttribute] string password
    )
    {
        if (string.IsNullOrEmpty(userName) == null)
        {
            return BadRequest("invalid user name.");
        }

        if (string.IsNullOrEmpty(password) == null)
        {
            return BadRequest("invalid password.");
        }

        //to do: verify if user already exists

        //to do: add hasing method to encript the password

        UserViewModel model = new  UserViewModel {
            UserName = userName,
            Password = password
        };

        _myDdContext.Users.Add(model);
        await _myDdContext.SaveChangesAsync();

        return CreatedAtAction(nameof(getAllUsers), new { id = model.Id }, model);
    }
}