using System.ComponentModel.DataAnnotations;

namespace VisitVisoko.Services.DTOs;

public class UpdateLocationDto
{
	[Required(ErrorMessage = "Naslov je obavezan.")]
	[StringLength(100, ErrorMessage = "Naslov ne smije biti duži od 100 karaktera.")]
	public string Title { get; set; } = string.Empty;

	[Required(ErrorMessage = "Adresa je obavezna.")]
	public string Address { get; set; } = string.Empty;

	[Required(ErrorMessage = "Opis je obavezan.")]
	public string Description { get; set; } = string.Empty;

	[Range(-180.0, 180.0, ErrorMessage = "Geografska dužina mora biti između -180 i 180.")]
	public double Longitude { get; set; }

	[Range(-90.0, 90.0, ErrorMessage = "Geografska širina mora biti između -90 i 90.")]
	public double Latitude { get; set; }

	public string ImageUrl { get; set; } = string.Empty;

	[Required(ErrorMessage = "Kategorija je obavezna.")]
	[Range(1, int.MaxValue, ErrorMessage = "Kategorija mora biti veća od 0.")]
	public int CategoryId { get; set; }
}