using CoolLibrary.Application.DTO;
using CoolLibrary.Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;

namespace CoolLibrary.API.Controllers;

/// <summary>
/// Authentication controller - handles user registration and login
/// These endpoints are public (do not require authentication)
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]  // ← Versioned route
[Produces("application/json")]
[Tags("🔐 Authentication")]
[ApiVersion("1.0")]  // ← This controller belongs to API v1.0
public class AuthController : ControllerBase
{
    // UserManager: ASP.NET Core Identity service for managing users
    // Provides methods like CreateAsync, FindByEmailAsync, CheckPasswordAsync
    private readonly UserManager<IdentityUser> _userManager;

    // TokenService: Our custom service to generate JWT tokens
    private readonly TokenService _tokenService;

    // Logger for tracking authentication events
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// Constructor - Dependency Injection provides these services
    /// </summary>
    public AuthController(
        UserManager<IdentityUser> userManager,
        TokenService tokenService,
        ILogger<AuthController> logger)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _logger = logger;
    }

    /// <summary>
    /// Register a new user account
    /// </summary>
    /// <remarks>
    /// Creates a new user in the system with the provided credentials.
    /// This endpoint is PUBLIC - no authentication required.
    /// 
    /// Request Sample:
    /// 
    ///     POST /api/auth/register
    ///     {
    ///         "email": "john.doe@example.com",
    ///         "password": "MySecurePassword123!",
    ///         "confirmPassword": "MySecurePassword123!"
    ///     }
    /// 
    /// Success Response:
    /// 
    ///     {
    ///         "message": "User registered successfully",
    ///         "email": "john.doe@example.com"
    ///     }
    /// 
    /// </remarks>
    /// <param name="registerDto">Registration data (email, password, confirmPassword)</param>
    /// <returns>Success message if registration is successful</returns>
    /// <response code="200">User registered successfully</response>
    /// <response code="400">Invalid data or email already exists</response>
    /// <response code="500">Internal server error</response>
    [HttpPost("register")]
    [AllowAnonymous]  // ← Public endpoint - no JWT required
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
    {
        // Step 1: Validate model state (DataAnnotations in RegisterDTO)
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Step 2: Check if user already exists
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "User with this email already exists" });
            }

            // Step 3: Create new IdentityUser
            var newUser = new IdentityUser
            {
                UserName = registerDto.Email,  // Username = Email
                Email = registerDto.Email,
                EmailConfirmed = true  // In production, you'd send a confirmation email
            };

            // Step 4: Create user in database (password is automatically hashed by Identity)
            var result = await _userManager.CreateAsync(newUser, registerDto.Password);

            // Step 5: Check if creation was successful
            if (!result.Succeeded)
            {
                // If failed, return validation errors from Identity
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { message = "User registration failed", errors });
            }

            // Step 6: Optionally assign default role (e.g., "User")
            // await _userManager.AddToRoleAsync(newUser, "User");

            _logger.LogInformation("New user registered: {Email}", registerDto.Email);

            return Ok(new 
            { 
                message = "User registered successfully",
                email = newUser.Email 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during user registration");
            return StatusCode(500, new { message = "An error occurred during registration" });
        }
    }

    /// <summary>
    /// Login with email and password
    /// </summary>
    /// <remarks>
    /// Authenticates a user and returns a JWT token if credentials are valid.
    /// This endpoint is PUBLIC - no authentication required.
    /// The token should be included in subsequent requests using the Authorization header.
    /// 
    /// Request Sample:
    /// 
    ///     POST /api/auth/login
    ///     {
    ///         "email": "john.doe@example.com",
    ///         "password": "MySecurePassword123!"
    ///     }
    /// 
    /// Success Response:
    /// 
    ///     {
    ///         "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    ///         "expiresAt": "2024-01-16T15:30:00Z",
    ///         "email": "john.doe@example.com",
    ///         "roles": ["User"]
    ///     }
    /// 
    /// How to use the token:
    /// 
    ///     Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
    /// 
    /// </remarks>
    /// <param name="loginDto">Login credentials (email and password)</param>
    /// <returns>JWT token and user information</returns>
    /// <response code="200">Login successful, returns JWT token</response>
    /// <response code="401">Invalid credentials</response>
    /// <response code="500">Internal server error</response>
    [HttpPost("login")]
    [AllowAnonymous]  // ← Public endpoint - no JWT required
    [ProducesResponseType(typeof(AuthResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        // Step 1: Validate model state
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Step 2: Find user by email
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                // Don't reveal whether the user exists (security best practice)
                return Unauthorized(new { message = "Invalid email or password" });
            }

            // Step 3: Verify password
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                _logger.LogWarning("Failed login attempt for user: {Email}", loginDto.Email);
                return Unauthorized(new { message = "Invalid email or password" });
            }

            // Step 4: Get user roles (if any)
            var roles = await _userManager.GetRolesAsync(user);

            // Step 5: Generate JWT token
            var token = _tokenService.GenerateJwtToken(user, roles);
            var expiresAt = _tokenService.GetTokenExpiration();

            _logger.LogInformation("User logged in successfully: {Email}", loginDto.Email);

            // Step 6: Return token and user information
            return Ok(new AuthResponseDTO
            {
                Token = token,
                ExpiresAt = expiresAt,
                Email = user.Email ?? string.Empty,
                Roles = roles.ToList()
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during user login");
            return StatusCode(500, new { message = "An error occurred during login" });
        }
    }
}
