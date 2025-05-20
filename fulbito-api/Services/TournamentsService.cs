using AutoMapper;
using fulbito_api.Dtos.Tournaments;
using fulbito_api.Models;
using fulbito_api.Repositories.Interfaces;
using fulbito_api.Sanitizers.Interfaces;
using fulbito_api.Services.Interfaces;


namespace fulbito_api.Services
{
	public class TournamentsService : ITournamentsService
	{
		readonly ITournamentsRepo repo;
		readonly IMapper mapper;
		readonly ISanitizer sanitizer;

		const int NameMaxLength = 48;
		const int MinPageSize = 5;
		const int MaxPageSize = 50;
		const int DefaultPageNumber = 1;

		const string KeyNotFoundMsgTemplate = "Tournament with id:{0} does not exist.";
		const string NameTooShortMsg = "Tournament name cannot be null or empty.";
		const string NameTooLongMsgTemplate = "Tournament name is too long. Max length is {0}";
		const string IdsDontMatchMsg = "Passed id and tournament id don't match.";


		public TournamentsService(ITournamentsRepo repo, IMapper mapper, ISanitizer sanitizer)
		{
			this.repo = repo;
			this.mapper = mapper;
			this.sanitizer = sanitizer;
		}


		public async Task<TournamentDto> Create(CreateTournamentDto createDto)
		{
			createDto.Name = sanitizer.Sanitize(createDto.Name);

			checkName(createDto.Name);

			var newTournament = mapper.Map<Tournament>(createDto);
			newTournament = await repo.Create(newTournament);

			return mapper.Map<TournamentDto>(newTournament);
		}

		public async Task Delete(int id)
		{
			if (id <= 0 || !await repo.Exists(id)) throw KeyNotFound(id);
			await repo.Delete(id);
		}

		public async Task<TournamentDto?> GetById(int id)
		{
			if (id <= 0) throw KeyNotFound(id);

			var tournament = await repo.Get(id);
			if (tournament is null) throw KeyNotFound(id);

			return mapper.Map<TournamentDto>(tournament);
		}

		public async Task<IEnumerable<TournamentDto>> GetManyById(IEnumerable<int> ids)
			=> mapper.Map<IEnumerable<TournamentDto>>(await repo.GetMany(ids));

		public async Task<IEnumerable<TournamentDto>> GetPage(int pageNumber, int pageSize)
		{
			pageNumber = Math.Max(pageNumber, DefaultPageNumber);	// forces requests to send pageNumber >= 1
			pageSize = Math.Clamp(pageSize, MinPageSize, MaxPageSize);

			var tournaments = await repo.GetPage(pageNumber: pageNumber - 1, pageSize: pageSize);	// page number in repo can be 0
			return mapper.Map<IEnumerable<TournamentDto>>(tournaments).ToList();
		}

		public async Task Update(int id, UpdateTournamentDto updateDto)
		{
			if (id != updateDto.Id) throw IdsDontMatch();

			updateDto.Name = sanitizer.Sanitize(updateDto.Name);

			checkName(updateDto.Name);

			if (!await repo.Exists(id)) throw KeyNotFound(id);
			
			var updatedTournament = mapper.Map<Tournament>(updateDto);

			await repo.Update(updatedTournament);
		}


		private void checkName(string name)
		{
			if (string.IsNullOrEmpty(name)) throw NameTooShort();
			if (name.Length > NameMaxLength) throw NameTooLong();
		}

		private KeyNotFoundException KeyNotFound(int id)
			=> new KeyNotFoundException(string.Format(KeyNotFoundMsgTemplate, id));

		private ArgumentNullException NameTooShort()
			=> new ArgumentNullException(NameTooShortMsg);

		private ArgumentOutOfRangeException NameTooLong()
			=> new ArgumentOutOfRangeException(string.Format(NameTooLongMsgTemplate, NameMaxLength));

		private ArgumentException IdsDontMatch()
			=> new ArgumentException(IdsDontMatchMsg);
	}
}
