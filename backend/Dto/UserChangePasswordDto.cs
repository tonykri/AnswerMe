namespace backend.Dto;

public class UserChangePasswordDto{
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? OldPassword { get; set; }
}