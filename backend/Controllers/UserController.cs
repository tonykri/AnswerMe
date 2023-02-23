using backend.Dto;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase{

    private readonly IUserService _userService;
    private readonly DataContext _dataContext;

    public UserController(IUserService userService, DataContext dataContext){
            _userService = userService;
            _dataContext = dataContext;
        }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto){
        var user = _dataContext.Users.Where(u => u.Email.Equals(userDto.Email)).FirstOrDefault();
        if (user is not null) return BadRequest("User already exists");

        var reg = _userService.Register(userDto);
        if (reg is not null) 
            return Ok(reg);
        return BadRequest("Something went wrong please check your data");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userDto){
        var user = _dataContext.Users.Where(u => u.Email.Equals(userDto.Email)).FirstOrDefault();
        if (user is null) return BadRequest("Account with current email does not exist");
        string token = _userService.Login(userDto);
        if (token is null) return BadRequest("Wrong password try again");
        return Ok(token);
    }

    [Authorize]
    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordDto passwordDto){
        string msg = _userService.ChangePassword(passwordDto);
        if (msg.Equals("ok")) return Ok(msg);
        return BadRequest(msg);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> Profile(){
        var user = _userService.GetUserInfo();
        return Ok(user);
    }

    [Authorize]
    [HttpDelete("deleteAccount")]
    public async Task<IActionResult> DeleteAccount(){
        var user = _userService.DeleteAccount();
        if (user is null) return BadRequest("Something went wrong");
        return Ok(user);
    }
}
