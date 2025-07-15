using Microsoft.AspNetCore.Mvc;
using RapPhim1.DTO.Auth;
using RapPhim1.Service;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto dto)
    {
        var result = await _userService.Register(dto);
        if (result != null) return BadRequest(result);
        return Ok("User registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        var token = await _userService.Login(dto);
        if (token == null) return Unauthorized("Invalid credentials");
        return Ok(new { token });
    }
}
