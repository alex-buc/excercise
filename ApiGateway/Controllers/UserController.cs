using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Data;
using Microsoft.EntityFrameworkCore;
using ApiGateway.Shared;

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
    [HttpGet("user")]
    public async Task<IActionResult> getUser([FromQueryAttribute] string user, [FromQueryAttribute] string password)
    {
        var foundUser = await _myDdContext.Users.FirstOrDefaultAsync(u => u.UserName == user && u.Password == PasswordHelper.GeneratePasswordWithStaticSalt(password));

        if (foundUser == null)
        {
            return NotFound("User not found or incorrect password.");
        }

        // Return the user data (you can also return only specific properties if needed)
        return Ok(foundUser);
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
            Password = PasswordHelper.GeneratePasswordWithStaticSalt(password)
        };

        _myDdContext.Users.Add(model);
        await _myDdContext.SaveChangesAsync();

        return CreatedAtAction(nameof(getAllUsers), new { id = model.Id }, model);
    }
   
}