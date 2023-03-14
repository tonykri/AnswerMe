namespace backend.Dto;

public class UserRegisterDto{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateOnly Birthdate { get; set; }
}