using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapPhim1.DTO.Common;
using RapPhim1.Models;
using RapPhim1.Service;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class ManageProfile : ControllerBase
{
    private readonly IUserService _userService;

    public ManageProfile(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var profile = await _userService.GetProfile(userId);
        if (profile == null) return NotFound();
        return Ok(profile);
    }

    [Authorize]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(UserUpdateProfileDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _userService.UpdateProfile(userId, dto);
        if (result != null) return BadRequest(result);
        return Ok("Profile updated");
    }

    [Authorize]
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _userService.ChangePassword(userId, dto);
        if (result != null) return BadRequest(result);
        return Ok("Password changed");
    }
}
