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
        var res = new UserLoginDataDtoDto(user.FirstName, user.LastName, user.Email, user.BirthDate, token);
        return Ok(res);
    }

    [Authorize]
    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordDto passwordDto){
        string msg = _userService.ChangePassword(passwordDto);
        if (msg.Equals("ok")) return Ok(msg);
        return BadRequest(msg);
    }

    [Authorize]
    [HttpGet("profile/{userId}")]
    public async Task<IActionResult> Profile([FromRoute] string userId){
        var user = _userService.GetUserInfo(userId);
        return Ok(user);
    }

    [Authorize]
    [HttpDelete("deleteAccount")]
    public async Task<IActionResult> DeleteAccount(){
        var user = _userService.DeleteAccount();
        if (user is null) return BadRequest("Something went wrong");
        return Ok(user);
    }

    [Authorize]
    [HttpPut("updateProfile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UserUpdateProfileDto userDto){
        var user = _userService.UpdateProfile(userDto);
        if (user is null) return BadRequest("Something went wrong");
        return Ok(user);
    }

    [Authorize]
    [HttpGet("activeToken")]
    public async Task<IActionResult> ActiveToken(){
        return Ok("Token is valid");
    }
}
