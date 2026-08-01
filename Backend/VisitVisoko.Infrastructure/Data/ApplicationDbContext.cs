using Microsoft.EntityFrameworkCore;
using VisitVisoko.Domain.Entities;

namespace VisitVisoko.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	public DbSet<Location> Locations => Set<Location>();
	public DbSet<Category> Categories => Set<Category>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Category>().HasData(
			new Category { Id = 1, Name = "Historija", IconName = "landmark" },
			new Category { Id = 2, Name = "Priroda", IconName = "tree" },
			new Category { Id = 3, Name = "Hrana", IconName = "utensils" }
		);
	}
}