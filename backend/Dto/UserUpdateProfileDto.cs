namespace backend.Dto;

public class UserUpdateProfileDto{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly BirthDate { get; set; }
}
