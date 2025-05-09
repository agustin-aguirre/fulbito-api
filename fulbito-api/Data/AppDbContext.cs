using fulbito_api.Models;
using Microsoft.EntityFrameworkCore;


namespace fulbito_api.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Tournament> Tournaments { get; set; }
		public DbSet<TournamentPlayerStats> TournamentsPlayerStats { get; set; }

		
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
	}
}
