using fulbito_api.Dtos.Tournaments;

namespace fulbito_api.Services.Interfaces
{
	public interface ITournamentsService
	{
		Task<TournamentDto> Create(CreateTournamentDto createDto);
		Task Delete(int id);
		Task<TournamentDto?> GetById(int id);
		Task<IEnumerable<TournamentDto>> GetManyById(IEnumerable<int> ids);
		Task Update(int id, UpdateTournamentDto updateDto);
		Task<IEnumerable<TournamentDto>> GetPage(int pageNumber, int pageSize);
	}
}