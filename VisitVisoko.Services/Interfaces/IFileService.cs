using Microsoft.AspNetCore.Http;

namespace VisitVisoko.Services;

public interface IFileService
{
	Task<string> SaveFileAsync(IFormFile file, string[] allowedExtensions);
	bool DeleteFile(string fileName);
}