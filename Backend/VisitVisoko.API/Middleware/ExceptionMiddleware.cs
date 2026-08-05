using System.Net;
using System.Text.Json;

namespace VisitVisoko.API.Middleware;

public class ExceptionMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ExceptionMiddleware> _logger;
	public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}
	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Greška: {Message}", ex.Message);
			await HandleExceptionAsync(context, ex);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception ex)
	{
		context.Response.ContentType = "application/json";

		var (statusCode, message) = ex switch
		{
			ArgumentException argEx => ((int)HttpStatusCode.BadRequest, argEx.Message),
			KeyNotFoundException NotFoundEx => ((int)HttpStatusCode.NotFound, NotFoundEx.Message),
			UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, "Pristup odbijen."),
			_ => ((int)HttpStatusCode.InternalServerError, "Došlo je do greške na serveru.")
		};

		context.Response.StatusCode = statusCode;

		var response = new
		{
			status = statusCode,
			message = message
		};

		return context.Response.WriteAsync(JsonSerializer.Serialize(response));
	}
}