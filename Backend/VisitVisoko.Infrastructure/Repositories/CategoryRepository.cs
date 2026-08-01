using System;
using System.Collections.Generic;
using System.Text;
using VisitVisoko.Domain.Entities;
using VisitVisoko.Domain.Interfaces;
using VisitVisoko.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace VisitVisoko.Infrastructure.Repositories;


public class CategoryRepository : ICategoryRepository
{
	private readonly ApplicationDbContext _context;

	public CategoryRepository(ApplicationDbContext context) { _context = context; }

	public async Task<IEnumerable<Category>> GetAllAsync()
	{
		return await _context.Categories.AsNoTracking().ToListAsync();
	}

	public async Task<Category?> GetByIdAsync(int id)
	{
		return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
	}

	public async Task AddAsync(Category c)
	{
		await _context.Categories.AddAsync(c);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Category c) 
	{
		_context.Categories.Update(c);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var category = await _context.Categories.FindAsync(id);
		if (category != null)
		{
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
		}
	}
}