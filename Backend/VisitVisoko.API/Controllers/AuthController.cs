using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using VisitVisoko.Services.DTOs;


namespace VisitVsisoko.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly IConfiguration _configuration;

	public AuthController(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	[HttpPost("Login")]
	public IActionResult Login([FromBody] LoginDto dto)
	{
		if (dto.Username == "admin" && dto.Password == "admin123")
		{
			var token = GenerateJwtToken(dto.Username, "Admin");
			return Ok(new { token });
		}

		return Unauthorized(new {message = "Neipravno korisnicko ime ili lozinka."});
	}

	private string GenerateJwtToken(string username, string role) 
	{
		var claims = new[]
		{
			new Claim(ClaimTypes.Name, username),
			new Claim(ClaimTypes.Role, role)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Audience"],
			claims: claims,
			expires: DateTime.Now.AddHours(4),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

}