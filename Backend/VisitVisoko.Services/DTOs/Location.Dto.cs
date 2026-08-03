namespace VisitVisoko.Services.DTOs;

public class LocationDto { 
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public double Latitude { get; set; }
	public double Longitude { get; set; }
	public string CategoryName { get; set; } = string.Empty;
	public string ImageUrl {  get; set; } = string.Empty;

	public int CategoryId { get; set; }
}
