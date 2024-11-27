using Microsoft.AspNetCore.Mvc;
using ApiGateway.Models;
using ApiGateway.Data;
using Microsoft.EntityFrameworkCore;
using ApiGateway.Shared;

namespace ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase 
{
    private readonly MyDbContext _myDdContext;

    public UsersController(MyDbContext myDdContext)
    {
        _myDdContext = myDdContext;
    }

    [HttpGet("")]
    public async Task<IActionResult> getAllUsers()
    {
        var allUsers = await _myDdContext.Users.ToListAsync();
        return Ok(allUsers);
    }

    [HttpGet("authentificate")]
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

    [HttpPost("")]
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
    
    [HttpGet("{userId}/staff")]
    public async Task<IActionResult> getStaffByUserId(int userId) {
        UserViewModel user = _myDdContext.Users.Find(userId);
        if (user == null) {
            return BadRequest("Invalid user id.");
        }

        StaffViewModel staff = await _myDdContext.Staffs.FirstOrDefaultAsync(s => s.User.Id == userId);
        if(staff == null) 
        {
            return NotFound("No staff found for user id.");
        }
        else 
        {
            return Ok(staff);
        }
    }
}