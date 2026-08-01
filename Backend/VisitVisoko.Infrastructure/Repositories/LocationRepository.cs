using Microsoft.EntityFrameworkCore;
using VisitVisoko.Domain.Entities;
using VisitVisoko.Domain.Interfaces;
using VisitVisoko.Infrastructure.Data;

namespace VisitVisoko.Infrastructure.Repositories;

public class LocationRepository : ILocationRepository {
	private readonly ApplicationDbContext _context;

	public LocationRepository(ApplicationDbContext context) { _context = context; }

	public async Task<IEnumerable<Location>> GetAllAsync()
	{
		return await _context.Locations.Include(l => l.Category).AsNoTracking().ToListAsync();
	}

	public async Task<Location?> GetByIdAsync(int id) { 
		return await _context.Locations.Include(l => l.Category).FirstOrDefaultAsync(l => l.Id == id);
	}

	public async Task AddAsync(Location location)
	{
		await _context.Locations.AddAsync(location);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Location location)
	{
		_context.Locations.Update(location);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id) 
	{
		var location = await _context.Locations.FindAsync(id);
		if (location != null) 
		{ 
			_context.Locations.Remove(location);
			await _context.SaveChangesAsync();
		}
	}
}