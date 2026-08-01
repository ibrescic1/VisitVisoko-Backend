namespace VisitVisoko.Domain.Entities;

public class Category
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string IconName { get; set; } = string.Empty;
	public ICollection<Location> Locations { get; set; } = new List<Location>();
}