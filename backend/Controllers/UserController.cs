using backend.Dto;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private readonly IUserService _userService;
    private readonly DataContext _dataContext;

    public UserController(IUserService userService, DataContext dataContext)
    {
        _userService = userService;
        _dataContext = dataContext;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDto loginUser)
    {
        var res = _userService.Login(loginUser);
        if (res is MsgStatus) return BadRequest(res);
        return Ok(res);
    }


    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRegisterDto registerUser)
    {
        MsgStatus res = _userService.Register(registerUser);
        if (res.StatusCode != 200) return BadRequest(res);
        return Ok(res);
    }


    [Authorize]
    [HttpPost("changePassword")]
    public IActionResult ChangePassword([FromBody] UserChangePasswordDto passwords)
    {
        MsgStatus res = _userService.ChangePassword(passwords);
        if (res.StatusCode != 200) return BadRequest(res);
        return Ok(res);
    }


    [Authorize]
    [HttpDelete("deleteAccount")]
    public IActionResult DeleteAccount(){
        return Ok(_userService.DeleteAccount());
    }


    [Authorize]
    [HttpPut("updateProfile")]
    public IActionResult UpdateProfile([FromBody] UserUpdateProfileDto userDto){
        MsgStatus res = _userService.UpdateProfile(userDto);
        if (res.StatusCode != 200) return BadRequest(res);
        return Ok(res);
    }


    [Authorize]
    [HttpGet("profile/{userEmail}")]
    public IActionResult UserProfile([FromRoute] string userEmail){
        var res = _userService.GetUserProfile(userEmail);
        if (res is MsgStatus) return BadRequest(res);
        return Ok(res);
    }


    [Authorize]
    [HttpGet("activeToken")]
    public IActionResult ActiveToken(){
        return Ok();
    }
}
