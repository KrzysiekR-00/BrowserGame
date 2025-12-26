using System.ComponentModel.DataAnnotations;

namespace Shared.Accounts;
public class RegisterRequestDto
{
    [Required]
    public string Username { get; set; } = "";

    [Required, MinLength(6)]
    public string Password { get; set; } = "";

    [Required, Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = "";
}
