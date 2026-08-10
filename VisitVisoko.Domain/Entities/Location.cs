namespace VisitVisoko.Domain.Entities;

public class Location
{
	public int Id {  get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public double Latitude { get; set; }
	public double Longitude { get; set; }
	public string ImageUrl { get; set; } = string.Empty;
	public bool isPopular {  get; set; }

	
	public int CategoryId { get; set; }
	public Category? Category { get; set; } = null;
}