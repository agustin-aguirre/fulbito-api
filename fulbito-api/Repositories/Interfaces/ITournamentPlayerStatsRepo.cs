using fulbito_api.Models;


namespace fulbito_api.Repositories.Interfaces
{
	public interface ITournamentPlayerStatsRepo : IIdentifiableEntityCRUDRepo<int, PlayerStatsFromTournament>
	{
		Task<IEnumerable<PlayerStatsFromTournament>> BulkAdd(IEnumerable<PlayerStatsFromTournament> playerStatsFromTournaments);
		Task<IEnumerable<PlayerStatsFromTournament>> GetTournamentPositions(int tournamentId);
	}
}