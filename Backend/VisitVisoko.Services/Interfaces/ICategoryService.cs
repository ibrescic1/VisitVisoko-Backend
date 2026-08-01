using System;
using System.Collections.Generic;
using System.Text;

using VisitVisoko.Services.DTOs;

namespace VisitVisoko.Services.Interfaces;

public interface ICategoryService
{
	Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
	Task<CategoryDto?> GetCategoryByIdAsync(int id);
	Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);
	Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto dto);
	Task<bool> DeleteCategoryAsync(int id);
}