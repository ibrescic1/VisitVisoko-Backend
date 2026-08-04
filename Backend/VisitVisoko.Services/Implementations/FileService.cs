using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace VisitVisoko.Services;

public class FileService : IFileService
{
	private readonly IWebHostEnvironment _environment;

	public FileService(IWebHostEnvironment environment)
	{
		_environment = environment;
	}
	public async Task<string> SaveFileAsync(IFormFile file, string[] allowedExtensions)
	{
		if (file == null || file.Length == 0)
			throw new ArgumentException("Fajl je prazan.");
		var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

		if (!allowedExtensions.Contains(extension))
			throw new ArgumentException($"Nedozvoljen format fajla. Dozvoljeni su: {string.Join(", ", allowedExtensions)}");
		var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

		var contentPath = _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
		var path = Path.Combine(contentPath, "uploads", "images");

		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}

		var fileName = $"{Guid.NewGuid()}{extension}";
		var fileNameWithPath = Path.Combine(path, fileName);

		using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
		{
			await file.CopyToAsync(stream);
		}

		return $"/uploads/images/{fileName}";
	}

	public bool DeleteFile(string relativePath)
	{
		if (string.IsNullOrEmpty(relativePath)) return false;

		var contentPath = _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
		var filePath = Path.Combine(contentPath, relativePath.TrimStart('/'));

		if (File.Exists(filePath))
		{
			File.Delete(filePath);
			return true;
		}

		return false;
	}
}