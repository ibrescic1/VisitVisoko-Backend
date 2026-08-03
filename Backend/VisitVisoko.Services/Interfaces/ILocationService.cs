using VisitVisoko.Services.DTOs;

namespace VisitVisoko.Services.Interfaces
{
	public interface ILocationService 
	{
		Task<IEnumerable<LocationDto>> GetAllLocationsAsync();
		Task<LocationDto?> GetLocationByIdAsync(int id);
		Task<LocationDto> CreateLocationAsync(CreateLocationDto dto);
		Task<bool> UpdateLocationAsync(int id, UpdateLocationDto dto);
		Task<bool> DeleteLocationAsync(int id);

		Task<IEnumerable<LocationDto>> GetLocationsByCategoryIdAsync(int categoryId);
	}
}
