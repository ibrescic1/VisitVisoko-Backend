using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VisitVisoko.Services.DTOs;
using VisitVisoko.Services.Interfaces;

namespace VisitVisoko.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriesController : ControllerBase
{
	private readonly ICategoryService _service;

	public CategoriesController(ICategoryService service) { _service = service; }

	[HttpGet]
	[AllowAnonymous]
	public async Task <IActionResult> GetAll()
	{
		var result = await _service.GetAllCategoriesAsync();
		return Ok(result);
	}

	[HttpGet("{id}")]
	[AllowAnonymous]
	public async Task <IActionResult> GetById(int id)
	{
		var result = await _service.GetCategoryByIdAsync(id);
		if (result == null) return NotFound();

		return Ok(result);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
	{
		var result = await _service.CreateCategoryAsync(dto);
		return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Update(int id,  [FromBody] UpdateCategoryDto dto)
	{
		var updated = await _service.UpdateCategoryAsync(id, dto);

		if (!updated) return NotFound();
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id) 
	{ 
		var deleted = await _service.DeleteCategoryAsync(id); 
		if (!deleted) return NotFound();

		return NoContent();
	}
}