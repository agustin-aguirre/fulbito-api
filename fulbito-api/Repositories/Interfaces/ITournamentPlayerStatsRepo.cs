using fulbito_api.Models;

namespace fulbito_api.Repositories.Interfaces
{
	public interface ITournamentPlayerStatsRepo
	{
		Task<TournamentPlayerStats?> Add(TournamentPlayerStats newPlayerStats);
		Task Delete(int statsRowId);
		Task DeletePlayerStats(int userId, int tournamentId);
		Task<TournamentPlayerStats?> Get(int statsRowId);
		Task<IEnumerable<TournamentPlayerStats>> GetAllPlayerStats(int userId);
		Task<TournamentPlayerStats?> GetPlayerStatsAtTournament(int userId, int tournamentId);
		Task<IEnumerable<TournamentPlayerStats?>> GetTournamentStats(int tournamentId);
		Task Update(int statsRowId, TournamentPlayerStats updatedPlayerStats);
		Task UpdatePlayerStats(int userId, int tournamentId, TournamentPlayerStats updatedPlayerStats);
	}
}