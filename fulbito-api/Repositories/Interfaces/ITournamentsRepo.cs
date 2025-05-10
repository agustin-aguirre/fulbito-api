using fulbito_api.Models;


namespace fulbito_api.Repositories.Interfaces
{
	public interface ITournamentsRepo<TId>
	{
		Task<Tournament> Create(Tournament newTournamentData);
		Task Delete(TId id);
		Task<bool> Exists(TId id);
		Task<Tournament?> Get(TId id);
		Task<IEnumerable<Tournament>> GetMany(int pageNumber, int pageSize);
		Task Update(int id, Tournament updatedTournamentData);
	}
}