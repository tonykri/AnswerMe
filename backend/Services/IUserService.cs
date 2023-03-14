using backend.Dto;
using backend.Models;

namespace backend.Services;

public interface IUserService
{
    MsgStatus Register(UserRegisterDto user);
    object Login(UserLoginDto user);
    MsgStatus ChangePassword(UserChangePasswordDto passwordDto);
    MsgStatus DeleteAccount();
    MsgStatus UpdateProfile(UserUpdateProfileDto userDto);
    object GetUserProfile(string userId);
}