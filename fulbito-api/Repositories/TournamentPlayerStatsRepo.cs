using fulbito_api.Data;
using fulbito_api.Errors;
using fulbito_api.Models;
using fulbito_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace fulbito_api.Repositories
{
	public class TournamentPlayerStatsRepo : ITournamentPlayerStatsRepo
	{
		readonly AppDbContext dbContext;

		public TournamentPlayerStatsRepo(AppDbContext dbContext) => this.dbContext = dbContext;


		public async Task<TournamentPlayerStats?> Get(int statsRowId)
			=> await dbContext.TournamentsPlayerStats.FirstOrDefaultAsync(s => s.Id == statsRowId);

		public async Task<IEnumerable<TournamentPlayerStats>> GetAllPlayerStats(int userId)
			=> await dbContext.TournamentsPlayerStats.Where(s => s.UserId == userId).ToListAsync();

		public async Task<TournamentPlayerStats?> GetPlayerStatsAtTournament(int userId, int tournamentId)
			=> await dbContext.TournamentsPlayerStats.FirstOrDefaultAsync(s => s.TournamentId == tournamentId && s.UserId == userId);

		public async Task<IEnumerable<TournamentPlayerStats?>> GetTournamentStats(int tournamentId)
			=> await dbContext.TournamentsPlayerStats.Where(s => s.TournamentId == tournamentId).ToListAsync();


		public async Task<TournamentPlayerStats?> Add(TournamentPlayerStats newPlayerStats)
		{
			await dbContext.TournamentsPlayerStats.AddAsync(newPlayerStats);
			return newPlayerStats;
		}


		public async Task Update(int statsRowId, TournamentPlayerStats updatedPlayerStats)
		{
			if ((await Get(updatedPlayerStats.Id)) is null) throw new EntityNotFoundException("Player Stats not registered.");
			dbContext.TournamentsPlayerStats.Update(updatedPlayerStats);
		}

		public async Task UpdatePlayerStats(int userId, int tournamentId, TournamentPlayerStats updatedPlayerStats)
		{
			bool entityNotFound = (await GetPlayerStatsAtTournament(updatedPlayerStats.Id, updatedPlayerStats.TournamentId)) is null;
			if (entityNotFound) throw new EntityNotFoundException("Player Stats not registered.");

			if (updatedPlayerStats.UserId != userId || updatedPlayerStats.TournamentId != tournamentId)
				throw new ArgumentException("Passed userId or tournamentId don't match with Player Stats.");

			dbContext.TournamentsPlayerStats.Update(updatedPlayerStats);
		}


		public async Task Delete(int statsRowId)
		{
			var target = await Get(statsRowId);
			if (target is null) throw new EntityNotFoundException("Player Stats not registered.");

			dbContext.TournamentsPlayerStats.Remove(target);
		}

		public async Task DeletePlayerStats(int userId, int tournamentId)
		{
			var target = await GetPlayerStatsAtTournament(userId, tournamentId);
			if (target is null) throw new EntityNotFoundException("Player Stats not registered.");

			dbContext.TournamentsPlayerStats.Remove(target);
		}
	}
}
