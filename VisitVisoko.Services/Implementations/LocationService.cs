using VisitVisoko.Domain.Interfaces;
using VisitVisoko.Services.DTOs;
using VisitVisoko.Services.Interfaces;
using VisitVisoko.Domain.Entities;

namespace VisitVisoko.Services.Implementations;

public class LocationService : ILocationService
{
	private readonly ILocationRepository _locationRepository;

	public LocationService(ILocationRepository locationRepository)
	{
		_locationRepository = locationRepository;
	}

	public async Task<IEnumerable<LocationDto>> GetAllLocationsAsync(string? search = null, int? categoryId = null, int page = 1, int pageSize = 10)
	{
		var locations = await _locationRepository.GetAllAsync();

		if (!string.IsNullOrWhiteSpace(search))
		{
			locations = locations.Where(l => l.Title.Contains(search, StringComparison.OrdinalIgnoreCase) || l.Description.Contains(search, StringComparison.OrdinalIgnoreCase));
		}

		if(categoryId.HasValue && categoryId.Value > 0)
		{
			locations = locations.Where(l => l.CategoryId == categoryId.Value);
		}
		
		locations = locations.Skip((page - 1) * pageSize).Take(pageSize);

		return locations.Select(l => new LocationDto
		{
			Id = l.Id,
			Title = l.Title,
			Description = l.Description,
			Address = l.Address,
			Latitude = l.Latitude,
			Longitude = l.Longitude,
			CategoryId = l.CategoryId, 
			CategoryName = l.Category != null ? l.Category.Name : string.Empty,
			ImageUrl = l.ImageUrl
		});
	}

	public async Task<LocationDto?> GetLocationByIdAsync(int id)
	{
		var l = await _locationRepository.GetByIdAsync(id);
		if (l == null) return null;

		return new LocationDto
		{
			Id = l.Id,
			Title = l.Title,
			Description = l.Description,
			Address = l.Address,
			Latitude = l.Latitude,
			Longitude = l.Longitude,
			CategoryId = l.CategoryId, 
			ImageUrl = l.ImageUrl,
			CategoryName = l.Category != null ? l.Category.Name : string.Empty
		};
	}

	public async Task<LocationDto> CreateLocationAsync(CreateLocationDto dto)
	{
		var location = new Location
		{
			Title = dto.Title,
			Description = dto.Description,
			Address = dto.Address,
			Latitude = dto.Latitude,
			Longitude = dto.Longitude,
			ImageUrl = dto.ImageUrl,
			CategoryId = dto.CategoryId
		};

		await _locationRepository.AddAsync(location);

		var savedLocation = await _locationRepository.GetByIdAsync(location.Id);

		return new LocationDto
		{
			Id = location.Id,
			Title = location.Title,
			Description = location.Description,
			Address = location.Address,
			Latitude = location.Latitude,
			Longitude = location.Longitude,
			ImageUrl = location.ImageUrl,
			CategoryId = location.CategoryId, 
			CategoryName = savedLocation?.Category != null ? savedLocation.Category.Name : string.Empty
		};
	}

	public async Task<bool> DeleteLocationAsync(int id)
	{
		var existing = await _locationRepository.GetByIdAsync(id);
		if (existing == null) return false;

		await _locationRepository.DeleteAsync(id);
		return true;
	}

	public async Task<bool> UpdateLocationAsync(int id, UpdateLocationDto dto)
	{
		var location = await _locationRepository.GetByIdAsync(id);
		if (location == null) return false;

		location.Title = dto.Title;
		location.Description = dto.Description;
		location.Address = dto.Address;
		location.Latitude = dto.Latitude;
		location.Longitude = dto.Longitude;
		location.ImageUrl = dto.ImageUrl;
		location.CategoryId = dto.CategoryId;

		await _locationRepository.UpdateAsync(location);
		return true;
	}

	public async Task<IEnumerable<LocationDto>> GetLocationsByCategoryIdAsync(int categoryId)
	{
		var locations = await _locationRepository.GetByCategoryIdAsync(categoryId);
		return locations.Select(l => new LocationDto
		{
			Id = l.Id,
			Title = l.Title,
			Description = l.Description,
			Address = l.Address,
			Latitude = l.Latitude,
			Longitude = l.Longitude,
			CategoryId = l.CategoryId, 
			ImageUrl = l.ImageUrl,
			CategoryName = l.Category != null ? l.Category.Name : string.Empty
		});
	}
}
