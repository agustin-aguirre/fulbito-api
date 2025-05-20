using fulbito_api.Data;
using fulbito_api.Models;
using fulbito_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fulbito_api.Repositories
{
	public class TournamentPlayerStatsRepo : IIdentifiableEntityCRUDRepo<int, TournamentPlayerStats>
	{
		readonly AppDbContext dbContext;


		public TournamentPlayerStatsRepo(AppDbContext dbContext) 
			=> this.dbContext = dbContext;


		public async Task<TournamentPlayerStats> Create(TournamentPlayerStats newPlayerStatsEntry)
		{
			//if (newPlayerStatsEntry.Id != 0)
				//throw new ArgumentException("Cannot create a player entry in tournament with Id other than 0");

			//if (!await tournamentsRepo.Exists(newPlayerStatsEntry.TournamentId)) 
				//throw new KeyNotFoundException($"Tournament with id:{newPlayerStatsEntry.TournamentId} does not exists.");

			//if (!await usersRepo.Exists(newPlayerStatsEntry.UserId)) 
				//throw new KeyNotFoundException($"User with id:{newPlayerStatsEntry.UserId} does not exists.");

			//if (await PlayerIsRegisteredAtTournament(newPlayerStatsEntry.UserId, newPlayerStatsEntry.TournamentId))
				//throw new Exception("Player is already registered at tournament.");

			//newPlayerStatsEntry.CreationDate = DateTime.Now;

			await dbContext.TournamentsPlayerStats.AddAsync(newPlayerStatsEntry);
			await persistChanges();
			return newPlayerStatsEntry;
		}

		public async Task<bool> Delete(int playerStatsEntryId)
		{
			//var existingEntry = await Get(playerStatsEntryId) ?? throw new KeyNotFoundException($"Player stats entry with id:{playerStatsEntryId} does not exist.");
			//dbContext.TournamentsPlayerStats.Remove(existingEntry);
			//await persistChanges();
			int affectedRows = await dbContext.TournamentsPlayerStats
					.Where(tps => tps.Id == playerStatsEntryId)
					.ExecuteDeleteAsync();
			return affectedRows > 0;
		}

		public async Task<bool> Exists(int playerStatsEntryId) 
			// return user is in cache || user is in db
			=> (await getFromCache(playerStatsEntryId)) is not null || await dbContext.Tournaments.AnyAsync(tps => tps.Id == playerStatsEntryId);

		public async Task<TournamentPlayerStats?> Get(int playerStatsEntryId) 
			=> await getFromCache(playerStatsEntryId) ?? await getFromDB(playerStatsEntryId);

		public async Task<IEnumerable<TournamentPlayerStats>> GetMany(IEnumerable<int> entityIds) 
			=> await dbContext.TournamentsPlayerStats.Where(tps => entityIds.Contains(tps.Id)).ToListAsync();

		public async Task<bool> PlayerIsRegisteredAtTournament(int userId, int tournamentId)
			=> await dbContext.TournamentsPlayerStats.AnyAsync(tps => tps.TournamentId == tournamentId && tps.UserId == userId);

		public async Task Update(TournamentPlayerStats updatedPlayerStatsEntry)
		{
			dbContext.TournamentsPlayerStats.Update(updatedPlayerStatsEntry);
			await persistChanges();
		}


		async Task<TournamentPlayerStats?> getFromCache(int id) => await dbContext.TournamentsPlayerStats.FindAsync(id);
		async Task<TournamentPlayerStats?> getFromDB(int id) => await dbContext.TournamentsPlayerStats.FirstOrDefaultAsync(t => t.Id == id);
		async Task persistChanges() => await dbContext.SaveChangesAsync();
	}
}