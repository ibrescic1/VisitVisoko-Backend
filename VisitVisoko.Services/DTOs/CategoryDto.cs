using System;
using System.Collections.Generic;
using System.Text;

namespace VisitVisoko.Services.DTOs;

public class CategoryDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string IconName { get; set; } = string.Empty;
}