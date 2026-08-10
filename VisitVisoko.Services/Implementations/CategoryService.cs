using System;
using System.Collections.Generic;
using System.Text;
using VisitVisoko.Services.DTOs;
using VisitVisoko.Domain.Entities;
using VisitVisoko.Domain.Interfaces;
using VisitVisoko.Services.Interfaces;

namespace VisitVisoko.Services.Implementations;

public class CategoryService : ICategoryService
{
	private readonly ICategoryRepository _categoryRepository;

	public CategoryService(ICategoryRepository categoryRepository) { _categoryRepository = categoryRepository; }

	public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
	{
		var categories = await _categoryRepository.GetAllAsync();

		return categories.Select(c => new CategoryDto
		{
			Id = c.Id,
			Name = c.Name,
			IconName = c.IconName
		});
	}


	public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
	{
		var c = await _categoryRepository.GetByIdAsync(id);
		if (c == null) return null;

		return new CategoryDto
		{
			Id = c.Id,
			Name = c.Name,
			IconName = c.IconName
		};

	}

	public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
	{
		var c = new Category
		{
			Name = dto.Name,
			IconName = dto.IconName
		};

		await _categoryRepository.AddAsync(c);

		return new CategoryDto
		{
			Id = c.Id,
			Name = c.Name,
			IconName = c.IconName
		};
	}

	public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto dto)
	{
		var category = await _categoryRepository.GetByIdAsync(id);
		if(category == null) return false;

		category.Name = dto.Name;
		category.IconName = dto.IconName;

		await _categoryRepository.UpdateAsync(category);
		return true;
	}

	public async Task<bool> DeleteCategoryAsync(int id) 
	{
		var category = await _categoryRepository.GetByIdAsync(id);
		if (category == null) return false;

		await _categoryRepository.DeleteAsync(id);
		return true;
	}
}