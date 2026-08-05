using Microsoft.AspNetCore.Mvc;
using VisitVisoko.Services;

namespace VisitVisoko.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
	private readonly IFileService _fileService;

	public UploadController(IFileService fileService)
	{
		_fileService = fileService;
	}

	[HttpPost("image")]
	public async Task<IActionResult> UploadImage(IFormFile file)
	{
		try
		{
			if (file == null || file.Length == 0)
				return BadRequest("Fajl nije poslan.");

			if (file.Length > 5 * 1024 * 1024)
				return BadRequest("Slika ne može biti veća od 5 MB.");

			var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
			var imageUrl = await _fileService.SaveFileAsync(file, allowedExtensions);

			return Ok(new { url = imageUrl });
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { message = "Greška pri spašavanju slike.", error = ex.Message });
		}
	}
}