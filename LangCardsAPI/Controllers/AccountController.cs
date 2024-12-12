using LangCardsAPI.Requests;
using LangCardsAPI.Responses;
using LangCardsAPI.Services;
using LangCardsDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LangCardsAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(UserManager<ApplicationUser> userManager, TokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<ApplicationUserResponse>> Login(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);
        if (user == null)
            return Unauthorized();

        var passwordCheck = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
        if (!passwordCheck)
            return Unauthorized();

        var displayName = $"{user.FirstName} {user.LastName}";
        var result = new ApplicationUserResponse(displayName, user.Email, _tokenService.CreateToken(user));

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<ApplicationUserResponse>> Register(ApplicationUserRegisterRequest registerRequest)
    {
        if (await _userManager.Users.AnyAsync(u => u.Email == registerRequest.Email))
            return BadRequest($"Email {registerRequest.Email} already exists");

        var newUser = new ApplicationUser
        {
            Email = registerRequest.Email,
            UserName = registerRequest.Email,
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName
        };

        var creationResult = await _userManager.CreateAsync(newUser, registerRequest.Password);
        if (creationResult.Succeeded) return Ok();
        return BadRequest($"User isn't registered:{creationResult}");
    }
}