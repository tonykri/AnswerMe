namespace backend.Dto;

public class UserUpdateProfileDto{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public DateOnly Birthdate { get; set; }
}
