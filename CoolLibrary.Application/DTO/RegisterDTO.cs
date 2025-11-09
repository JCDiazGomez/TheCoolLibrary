using System.ComponentModel.DataAnnotations;

namespace CoolLibrary.Application.DTO;

/// <summary>
/// Data Transfer Object for user registration
/// Contains the required information to create a new user account
/// </summary>
public class RegisterDTO
{
    /// <summary>
    /// User's email address - will be used as username for login
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User's password - must meet security requirements
    /// (minimum 6 characters, at least one uppercase, one lowercase, one digit)
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Password confirmation - must match the Password field
    /// This prevents typos when creating an account
    /// </summary>
    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
