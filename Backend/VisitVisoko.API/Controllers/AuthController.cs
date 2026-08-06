using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using VisitVisoko.Services.DTOs;
using VisitVisoko.Infrastructure.Data;

namespace VisitVisoko.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly IConfiguration _configuration;
	private readonly ApplicationDbContext _context;

	public AuthController(IConfiguration configuration, ApplicationDbContext context)
	{
		_configuration = configuration;
		_context = context;
	}

	[HttpPost("Login")]
	public async Task<IActionResult> Login([FromBody] LoginDto dto)
	{
		var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);

		if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
		{
			return Unauthorized(new { message = "Neispravno korisničko ime ili lozinka." });
		}

		var token = GenerateJwtToken(user.Id, user.Username, user.Role);
		return Ok(new { token });
	}

	private string GenerateJwtToken(int userId, string username, string role)
	{
		var claims = new[]
		{
			new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
			new Claim(ClaimTypes.Name, username),
			new Claim(ClaimTypes.Role, role)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddHours(4),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}