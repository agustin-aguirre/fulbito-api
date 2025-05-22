using fulbito_api.Data;
using fulbito_api.Models;
using fulbito_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace fulbito_api.Repositories
{
	public class TournamentPlayerStatsRepo : ITournamentPlayerStatsRepo
	{
		readonly AppDbContext dbContext;

		public TournamentPlayerStatsRepo(AppDbContext dbContext) => this.dbContext = dbContext;


		public async Task<IEnumerable<PlayerStatsFromTournament>> BulkAdd(IEnumerable<PlayerStatsFromTournament> playerStatsFromTournaments)
		{
			await dbContext.TournamentsPlayerStats.AddRangeAsync(playerStatsFromTournaments);
			await persistChanges();
			return playerStatsFromTournaments;
		}

		public async Task<PlayerStatsFromTournament> Create(PlayerStatsFromTournament newPlayerEntry)
		{
			await dbContext.TournamentsPlayerStats.AddAsync(newPlayerEntry);
			await persistChanges();
			return newPlayerEntry;
		}

		public async Task<bool> Delete(int playerEntryId)
		{
			int affectedRows = await dbContext.TournamentsPlayerStats
				.Where(e => e.Id == playerEntryId)
				.ExecuteDeleteAsync();
			return affectedRows > 0;
		}

		public async Task<bool> Exists(int playerEntryId)
			// return user is in cache || user is in db
			=> (await getFromCache(playerEntryId)) is not null || await dbContext.TournamentsPlayerStats.AnyAsync(e => e.Id == playerEntryId);

		public async Task<PlayerStatsFromTournament?> Get(int playerEntryId)
			// Primero busca a la entidad en la caché de EF.
			// Si la entidad está siendo trackeada, retorna eso.
			// Sino, realiza la consulta a la base de datos.
			=> await getFromCache(playerEntryId) ?? await getFromDB(playerEntryId);

		public async Task<IEnumerable<PlayerStatsFromTournament>> GetMany(IEnumerable<int> playerEntryIds)
			=> await dbContext.TournamentsPlayerStats.Where(e => playerEntryIds.Contains(e.Id)).ToListAsync();

		public async Task<IEnumerable<PlayerStatsFromTournament>> GetTournamentPositions(int tournamentId)
			=> await dbContext.TournamentsPlayerStats
				.Where(e => e.TournamentId == tournamentId)
				.OrderBy(e => e.TotalPoints)
				.ToListAsync();

		public async Task Update(PlayerStatsFromTournament updatedPlayerEntry)
		{
			// fuerza cachear la entidad con Get (la encuentra en caché o la carga de BBDD)
			var cachedEntry = await Get(updatedPlayerEntry.Id);
			if (cachedEntry is not null)
				dbContext.Entry(cachedEntry).CurrentValues.SetValues(updatedPlayerEntry);
			await persistChanges();
		}


		async Task<PlayerStatsFromTournament?> getFromCache(int id) => await dbContext.TournamentsPlayerStats.FindAsync(id);
		async Task<PlayerStatsFromTournament?> getFromDB(int id) => await dbContext.TournamentsPlayerStats.FirstOrDefaultAsync(u => u.Id == id);
		async Task persistChanges() => await dbContext.SaveChangesAsync();
	}
}