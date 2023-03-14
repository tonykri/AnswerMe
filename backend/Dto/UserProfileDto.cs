namespace backend.Dto;

public class UserProfileDto{
    public UserProfileDto(string firstname, string lastname, string email, DateOnly birthdate){
        Firstname = firstname;
        Lastname = lastname;
        Birthdate = birthdate;
        Email = email;
    }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public DateOnly Birthdate { get; set; }
}