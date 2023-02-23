using backend.Dto;
using backend.Models;

namespace backend.Services;

public interface IUserService{
    UserProfileDto Register(UserRegisterDto user);
    string Login(UserLoginDto user);
    string ChangePassword(UserChangePasswordDto passwordDto);
    UserProfileDto GetUserInfo();
    User DeleteAccount();
}