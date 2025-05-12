using fulbito_api.Dtos.Tournaments;
using System.Threading.Tasks;

namespace fulbito_api.Services.Interfaces
{
	public interface ITournamentsService
	{
		Task<TournamentDto> Create(CreateTournamentDto createTournamentDto);
		Task Delete(int id);
		Task<TournamentDto?> Get(int id);
		Task<IEnumerable<TournamentDto>> GetPage(int pageNumber, int pageSize);
		Task Update(int id, UpdateTournamentDto updateTournamentDto);
	}
}