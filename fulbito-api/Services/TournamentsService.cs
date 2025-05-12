using AutoMapper;
using fulbito_api.Dtos.Tournaments;
using fulbito_api.Errors;
using fulbito_api.Models;
using fulbito_api.Repositories.Interfaces;
using fulbito_api.Services.Helpers;
using fulbito_api.Services.Interfaces;


namespace fulbito_api.Services
{
	public class TournamentsService : ITournamentsService
	{
		readonly ITournamentsRepo<int> repo;
		readonly IMapper mapper;


		public TournamentsService(ITournamentsRepo<int> repo, IMapper mapper)
		{
			this.repo = repo;
			this.mapper = mapper;
		}


		public async Task<TournamentDto?> Get(int id)
		{
			var targetTournament = await repo.Get(id);

			if (targetTournament == null) return null;

			return mapper.Map<TournamentDto>(targetTournament);
		}


		public async Task<IEnumerable<TournamentDto>> GetPage(int pageNumber, int pageSize)
			=> mapper.Map<IEnumerable<TournamentDto>>(await repo.GetMany(pageNumber, pageSize));


		public async Task<TournamentDto> Create(CreateTournamentDto createTournamentDto)
		{
			checkTotalMatches(createTournamentDto.TotalMatches);
			checkDates(createTournamentDto.StartDate, createTournamentDto.EndDate);

			var newTournament = mapper.Map<Tournament>(createTournamentDto);
			checkPointsAssignment(newTournament, new TournamentPointsHelper(createTournamentDto));

			await repo.Create(newTournament);

			return mapper.Map<TournamentDto>(newTournament);
		}


		public async Task Update(int id, UpdateTournamentDto updateTournamentDto)
		{
			if (updateTournamentDto.TotalMatches != null)
				checkTotalMatches((int)updateTournamentDto.TotalMatches);
			
			checkDates(updateTournamentDto.StartDate, updateTournamentDto.EndDate);

			if (updateTournamentDto.TotalMatches != null)
				checkTotalMatches((int)updateTournamentDto.TotalMatches);

			var mappedTournament = mapper.Map<Tournament>(updateTournamentDto);
			checkPointsAssignment(mappedTournament, new TournamentPointsHelper(updateTournamentDto));

			await repo.Update(id, mappedTournament);
		}


		public async Task Delete(int id) => await repo.Delete(id);



		void checkDates(DateTime? startDate, DateTime? endDate)
		{
			bool hasStartDate = startDate != null;
			bool hasEndDate = endDate != null;
			if (!hasStartDate && hasEndDate)
				throw new ArgumentOutOfRangeException("Can't set an End Date before setting a Start Date.");
			if (hasStartDate && hasEndDate && startDate > endDate)
				throw new ArgumentOutOfRangeException("Start Date is set after End Date.");
		}

		void assignOrFail(Func<int?> valueRedux, Action valueSetter, Action failureChecks)
		{
			if (valueRedux() == null)
			{
				valueSetter();
			}
			else
			{
				failureChecks();
			}
		}

		void checkTotalMatches(int totalMatches)
		{
			if (totalMatches < 1) throw new ArgumentOutOfRangeException("Can't set total matches to less than 1.");
		}

		void checkPointsAssignment(Tournament tournament, TournamentPointsHelper pointsHelper)
		{
			// win points
			assignOrFail(
				() => pointsHelper.PointsPerWonMatch,
				() => tournament.PointsPerWonMatch = 0,
				() =>
				{
					if (tournament.PointsPerWonMatch < 0)
						throw new ArgumentOutOfRangeException("Cannot set negative points per won match");
				}
			);

			// tie points
			assignOrFail(
				() => pointsHelper.PointsPerTiedMatch,
				() => tournament.PointsPerTiedMatch = 0,
				() =>
				{
					if (tournament.PointsPerTiedMatch < 0)
						throw new ArgumentOutOfRangeException("Cannot set negative points per tied match.");
					if (tournament.PointsPerWonMatch < tournament.PointsPerTiedMatch)
						throw new ArgumentOutOfRangeException("Cannot set less points per tie than points per won matches.");
				}
			);

			// loss points
			assignOrFail(
				() => pointsHelper.PointsPerLostMatch,
				() => tournament.PointsPerLostMatch = 0,
				() =>
				{
					if (tournament.PointsPerLostMatch >= tournament.PointsPerTiedMatch)
						throw new ArgumentOutOfRangeException("Cannot set more or equal points per loss than tied matches.");
				}
			);

			// assert points
			assignOrFail(
				() => pointsHelper.PointsPerAssertedMatch,
				() => tournament.PointsPerAssertedMatch = 0,
				() =>
				{
					if (tournament.PointsPerAssertedMatch < 0)
						throw new ArgumentOutOfRangeException("Cannot set negative points per asserted match.");
				}
			);
		}
	}
}
