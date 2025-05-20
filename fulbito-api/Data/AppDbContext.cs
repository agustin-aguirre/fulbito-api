using fulbito_api.Models;
using fulbito_api.Models.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace fulbito_api.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Tournament> Tournaments { get; set; }
		public DbSet<TournamentPlayerStats> TournamentsPlayerStats { get; set; }

		
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (var entry in ChangeTracker.Entries<IAuditable>())
			{
				if (entry.State == EntityState.Added)
				{
					entry.Property(e => e.CreationDate).CurrentValue = DateTime.UtcNow;
				}

				if (entry.State == EntityState.Modified)
				{
					entry.Property(e => e.LastModification).CurrentValue = DateTime.UtcNow;
				}
			}
			return await base.SaveChangesAsync(cancellationToken);
		}

	}
}
