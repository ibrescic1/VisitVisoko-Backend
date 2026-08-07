using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace VisitVisoko.Services.DTOs;

public class CreateCategoryDto
{
	[Required(ErrorMessage = "Naziv je obavezan.")]
	[StringLength(100, ErrorMessage = "Naziv ne smije biti duži od 100 karaktera.")]
	public string Name { get; set; } = string.Empty;
	public string IconName { get; set; } = string.Empty;
}