namespace backend.Dto;

public class UserProfileDto{
    public UserProfileDto(string firstname, string lastname, string email, DateOnly birthdate){
        FirstName = firstname;
        LastName = lastname;
        BirthDate = birthdate;
        Email = email;
    }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateOnly BirthDate { get; set; }
}