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
			var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
			var imageUrl = await _fileService.SaveFileAsync(file, allowedExtensions);

			return Ok(new { url = imageUrl });		
	}
}