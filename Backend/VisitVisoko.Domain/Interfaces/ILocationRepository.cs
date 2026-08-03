using VisitVisoko.Domain.Entities;
namespace VisitVisoko.Domain.Interfaces;

public interface ILocationRepository
{
	Task<IEnumerable<Location>> GetAllAsync();
	Task<Location?> GetByIdAsync(int id);
	Task AddAsync(Location location);
	Task UpdateAsync(Location location);
	Task DeleteAsync(int id);
	Task <IEnumerable<Location>>GetByCategoryIdAsync(int categoryId);
}