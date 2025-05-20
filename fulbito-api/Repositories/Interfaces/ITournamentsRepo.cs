using fulbito_api.Models;


namespace fulbito_api.Repositories.Interfaces
{
	public interface ITournamentsRepo : IIdentifiableEntityCRUDRepo<int, Tournament>
	{
		Task<IEnumerable<Tournament>> GetPage(int pageNumber, int pageSize);
	}
}