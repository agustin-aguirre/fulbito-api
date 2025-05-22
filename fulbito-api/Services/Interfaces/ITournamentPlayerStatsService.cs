using fulbito_api.Dtos.Tournaments;
using fulbito_api.Models;

namespace fulbito_api.Services.Interfaces
{
	public interface ITournamentPlayerStatsService
	{
		Task<IEnumerable<PlayerStatsFromTournamentDto>> BulkRegisterPlayersToTournament(IEnumerable<int> playerIds, int tournamentId);
		Task Delete(int playerEntryId);
		Task<IEnumerable<PlayerStatsFromTournament>> GetPositions(int tournamentId);
		Task<PlayerStatsFromTournamentDto> RegisterPlayerToTournament(int userId, int tournamentId);
		Task UpdatePlayerEntry(int playerEntryId, UpdatePlayerStatsFromTournamentDto updatedPlayerEntry);
	}
}