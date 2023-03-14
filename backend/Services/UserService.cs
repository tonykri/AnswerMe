using System.Security.Claims;
using backend.Dto;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class UserService : IUserService
{
    private readonly Hashing _hashing;
    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserService(DataContext dataContext, Hashing hashing, IHttpContextAccessor httpContextAccessor)
    {
        _hashing = hashing;
        _dataContext = dataContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public object Login(UserLoginDto loginUser)
    {
        User? user = _dataContext.Users.Where(p => p.Email == loginUser.Email).FirstOrDefault();
        if (user is null) return new MsgStatus("Account with current email does not exist", 400);
        byte[]? PasswordHash = user.PasswordHash;
        byte[]? PasswordSalt = user.PasswordSalt;

        if (!_hashing.VerifyPasswordHash(loginUser.Password, PasswordHash, PasswordSalt))
            return new MsgStatus("Wrong password", 400);

        string token = _hashing.CreateToken(user);
        return new UserLoginDataDtoDto(user.Firstname, user.Lastname, user.Email, user.Birthdate, token);
    }

    public MsgStatus Register(UserRegisterDto registerUser)
    {
        if (registerUser.Birthdate.AddYears(18).Year > DateTime.Now.Year ||
                    registerUser.Birthdate.Year < 1950)
            return new MsgStatus("Not valid age", 400);
        
        registerUser.Password = registerUser.Password.Trim();
        if (registerUser.Password.Length < 5) return new MsgStatus("Password must be at least 5 characters", 400);

        User user = new User();
        _hashing.CreatePasswordHash(registerUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

        user.Birthdate = registerUser.Birthdate;
        user.Email = registerUser.Email;
        user.Firstname = registerUser.Firstname;
        user.Lastname = registerUser.Lastname;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _dataContext.Add(user);
        _dataContext.SaveChanges();

        return new MsgStatus("Account created", 200);
    }

    public MsgStatus ChangePassword(UserChangePasswordDto passwordDto)
    {
        if (!passwordDto.Password.Equals(passwordDto.ConfirmPassword)) return new MsgStatus("Passwords do not match", 400);
        User user = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();

        if (!_hashing.VerifyPasswordHash(passwordDto.OldPassword, user.PasswordHash, user.PasswordSalt))
            return new MsgStatus("Current password is not correct", 400);

        passwordDto.Password = passwordDto.Password.Trim();
        if (passwordDto.Password.Length < 5) return new MsgStatus("Password must be at least 5 characters", 400);

        _hashing.CreatePasswordHash(passwordDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        _dataContext.SaveChanges();
        return new MsgStatus("Password changed", 200);
    }

    public MsgStatus DeleteAccount(){
        User user = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();
        _dataContext.Remove(user);
        _dataContext.SaveChanges();
        return new MsgStatus("Account deleted", 200);
    }

    public MsgStatus UpdateProfile(UserUpdateProfileDto userDto){
        User user = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();

        if (userDto.Birthdate.AddYears(18).Year > DateTime.Now.Year ||
                    userDto.Birthdate.Year < 1950)
            return new MsgStatus("Not valid age", 400);
        
        userDto.Firstname = userDto.Firstname.Trim();
        userDto.Lastname = userDto.Lastname.Trim();

        if (userDto.Firstname.Length < 1 || userDto.Lastname.Length < 1) return new MsgStatus("Fields cannot be blank", 400);

        user.Birthdate = userDto.Birthdate;
        user.Firstname = userDto.Firstname;
        user.Lastname = userDto.Lastname;
        _dataContext.SaveChanges();
        return new MsgStatus("Profile updated", 200);
    }


    public object GetUserProfile(string userEmail){
        User user = _dataContext.Users.Where(u => u.Email == userEmail).Include(u => u.Posts).FirstOrDefault();
        if (user is null) return new MsgStatus("User does not exist", 404);
        return new UserProfileWithPostsDto(user.Firstname, user.Lastname, user.Email, user.Birthdate, user.Posts);
    }
}
