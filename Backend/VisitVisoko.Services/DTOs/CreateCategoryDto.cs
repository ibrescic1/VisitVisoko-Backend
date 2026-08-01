using System;
using System.Collections.Generic;
using System.Text;

namespace VisitVisoko.Services.DTOs;

public class CreateCategoryDto
{
	public string Name { get; set; } = string.Empty;
	public string IconName { get; set; } = string.Empty;
}