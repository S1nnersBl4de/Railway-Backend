using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Railwaybackproject.DTO.Authentication;
using Railwaybackproject.Services.abstractions;
using Railwaybackproject.Models;
using Railwaybackproject.Services.implementations;
using Railwaybackproject.DTO.Authentication;

namespace Railwaybackproject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserservice _userService;

    public AuthController(IUserservice userService)
    {
        _userService = userService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request) //register request
    {
        var response = await _userService.RegisterUser(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request) //login request
    {
        var response = await _userService.LoginUser(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}
