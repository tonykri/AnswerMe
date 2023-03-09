namespace backend.Dto;

public class UserLoginDataDtoDto : UserProfileDto
{
    public UserLoginDataDtoDto(string firstname, string lastname, string email, DateOnly birthdate, string token) : base(firstname, lastname, email, birthdate)
    {
        Token = token;
    }
    public string? Token { get; set; }
}