using System.Security.Claims;
using backend.Dto;
using backend.Models;

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

    public string Login(UserLoginDto loginUser)
    {
        User? user = _dataContext.Users.Where(p => p.Email == loginUser.Email).FirstOrDefault();
        byte[]? PasswordHash = user.PasswordHash;
        byte[]? PasswordSalt = user.PasswordSalt;

        if (!_hashing.VerifyPasswordHash(loginUser.Password, PasswordHash, PasswordSalt))
            return null;

        return _hashing.CreateToken(user);  
    }

    public UserProfileDto Register(UserRegisterDto registerUser)
    {
        if (registerUser.BirthDate.AddYears(18).Year > DateTime.Now.Year || 
                    registerUser.BirthDate.Year < 1950) 
            return null;
        if (registerUser.Email is null || registerUser.Password is null || 
                    registerUser.FirstName is null || registerUser.LastName is null)
            return null;
        
        
        User user = new User();
        _hashing.CreatePasswordHash(registerUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

        user.BirthDate = registerUser.BirthDate;
        user.Email = registerUser.Email;
        user.FirstName = registerUser.FirstName;
        user.LastName = registerUser.LastName;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _dataContext.Add(user);
        _dataContext.SaveChanges();

        return new UserProfileDto(user.FirstName, user.LastName, user.Email, user.BirthDate);
    }

    public string ChangePassword(UserChangePasswordDto passwordDto){
        if (passwordDto.Password is null) return "Password is required";
        if (!passwordDto.Password.Equals(passwordDto.ConfirmPassword)) return "Passwords don't match";
        User user = _dataContext.Users.Where(p => p.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();
        if (user is null)
            return "something went wrong";
        if (!_hashing.VerifyPasswordHash(passwordDto.OldPassword, user.PasswordHash, user.PasswordSalt))
            return "Current password is not correct";

        _hashing.CreatePasswordHash(passwordDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        _dataContext.SaveChanges();
        return "ok";
    }

    public UserProfileDto GetUserInfo(){
        User user = _dataContext.Users.Where(p => p.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();
        var obj = new UserProfileDto(user.FirstName, user.LastName, user.Email, user.BirthDate);
        return obj;
    }
}