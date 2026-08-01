using Microsoft.AspNetCore.Mvc;
using VisitVisoko.Services.Interfaces;
using VisitVisoko.Services.DTOs;

namespace VisitVisoko.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
	private readonly ILocationService _locationService;

	public LocationsController(ILocationService locationService)
	{
		_locationService = locationService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var locations = await _locationService.GetAllLocationsAsync();
		return Ok(locations);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id) 
	{
		var location = await _locationService.GetLocationByIdAsync(id);
		if (location == null) return NotFound();
		return Ok(location);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateLocationDto dto)
	{
		var result = await _locationService.CreateLocationAsync(dto);
		return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete (int id) 
	{
		var deleted = await _locationService.DeleteLocationAsync(id);
		if (!deleted) return NotFound();
		return NoContent();
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Update (int id, [FromBody] UpdateLocationDto dto)
	{
		var updated = await _locationService.UpdateLocationAsync(id, dto);
		if (!updated) return NotFound();
		return NoContent();
	}
}