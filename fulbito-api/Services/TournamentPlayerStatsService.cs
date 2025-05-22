using AutoMapper;
using fulbito_api.Dtos.Tournaments;
using fulbito_api.Models;
using fulbito_api.Repositories.Interfaces;
using fulbito_api.Services.Interfaces;


namespace fulbito_api.Services
{
	public class TournamentPlayerStatsService : ITournamentPlayerStatsService
	{
		readonly IUsersRepo usersRepo;
		readonly ITournamentsRepo tournamentsRepo;
		readonly ITournamentPlayerStatsRepo playerEntriesRepo;
		readonly IMapper mapper;

		const string KeyNotFoundMsgTemplate = "{0} with id:{1} does not exist.";
		const string IdsDontMatchMsg = "Passed id and Tournament Entry dont match";


		public TournamentPlayerStatsService(
			IUsersRepo usersRepo,
			ITournamentsRepo tournamentsRepo,
			ITournamentPlayerStatsRepo playerEntriesRepo,
			IMapper mapper)
		{
			this.usersRepo = usersRepo;
			this.tournamentsRepo = tournamentsRepo;
			this.playerEntriesRepo = playerEntriesRepo;
			this.mapper = mapper;
		}


		public async Task<PlayerStatsFromTournamentDto> RegisterPlayerToTournament(int userId, int tournamentId)
		{
			Task<bool>[] existsOps =
			{
				tournamentsRepo.Exists(tournamentId),
				usersRepo.Exists(userId)
			};
			bool[] results = await Task.WhenAll(existsOps);
			var (tournamentExists, userExists) = (results[0], results[1]);
			if (!tournamentExists) throw TournamentNotFound(tournamentId);
			if (!userExists) throw PlayerNotFound(userId);

			var newPlayerEntry = new PlayerStatsFromTournament()
			{
				TournamentId = tournamentId,
				UserId = userId,
			};
			await playerEntriesRepo.Create(newPlayerEntry);
			return mapper.Map<PlayerStatsFromTournamentDto>(playerEntriesRepo);
		}

		public async Task<IEnumerable<PlayerStatsFromTournamentDto>> BulkRegisterPlayersToTournament(IEnumerable<int> playerIds, int tournamentId)
		{
			if (!await tournamentsRepo.Exists(tournamentId)) throw TournamentNotFound(tournamentId);
			var existingPlayerIds = await usersRepo.BulkExists(playerIds);
			int? firstNonExistingPlayerId = playerIds.FirstOrDefault(id => !existingPlayerIds.Contains(id));
			if (firstNonExistingPlayerId is not null) throw PlayerNotFound(firstNonExistingPlayerId.Value);

			var newPlayerEntries = playerIds.Select(
				id => new PlayerStatsFromTournament()
				{
					TournamentId = tournamentId,
					UserId = id
				});

			newPlayerEntries = await playerEntriesRepo.BulkAdd(newPlayerEntries);
			return mapper.Map<IEnumerable<PlayerStatsFromTournamentDto>>(newPlayerEntries);
		}


		public async Task<IEnumerable<PlayerStatsFromTournament>> GetPositions(int tournamentId)
		{
			if (!await tournamentsRepo.Exists(tournamentId)) throw TournamentNotFound(tournamentId);
			var positions = await playerEntriesRepo.GetTournamentPositions(tournamentId);
			return mapper.Map<IEnumerable<PlayerStatsFromTournament>>(positions);
		}


		public async Task UpdatePlayerEntry(int playerEntryId, UpdatePlayerStatsFromTournamentDto updatedPlayerEntry)
		{
			if (playerEntryId != updatedPlayerEntry.Id) throw IdsDontMatch();

			var existingPlayerEntry = await playerEntriesRepo.Get(playerEntryId);
			if (existingPlayerEntry is null) throw EntryNotFound(playerEntryId);

			// cannot change entry to set another tournament or set it to another user
			if (existingPlayerEntry.TournamentId != updatedPlayerEntry.TournamentId) throw IdsDontMatch();
			if (existingPlayerEntry.UserId != updatedPlayerEntry.UserId) throw IdsDontMatch();

			// forces to not set a less than 0 value
			updatedPlayerEntry.GoalCount = Math.Max(updatedPlayerEntry.GoalCount, 0);
			updatedPlayerEntry.AssitenciesCount = Math.Max(updatedPlayerEntry.AssitenciesCount, 0);
			updatedPlayerEntry.PlayedMatchesCount = Math.Max(updatedPlayerEntry.PlayedMatchesCount, 0);
			updatedPlayerEntry.WonMatchesCount = Math.Max(updatedPlayerEntry.WonMatchesCount, 0);
			updatedPlayerEntry.TiedMatchesCount = Math.Max(updatedPlayerEntry.TiedMatchesCount, 0);
			updatedPlayerEntry.LostMatchesCount = Math.Max(updatedPlayerEntry.LostMatchesCount, 0);
			updatedPlayerEntry.AssertedMatchesCount = Math.Max(updatedPlayerEntry.AssertedMatchesCount, 0);

			await playerEntriesRepo.Update(mapper.Map<PlayerStatsFromTournament>(updatedPlayerEntry));
		}

		public async Task Delete(int playerEntryId)
		{
			if (!await playerEntriesRepo.Exists(playerEntryId)) throw EntryNotFound(playerEntryId);
			await playerEntriesRepo.Delete(playerEntryId);
		}


		private KeyNotFoundException TournamentNotFound(int id) => KeyNotFound("Tournament", id);
		private KeyNotFoundException PlayerNotFound(int id) => KeyNotFound("Player", id);
		private KeyNotFoundException EntryNotFound(int id) => KeyNotFound("Player Stats from Tournmaent", id);

		private KeyNotFoundException KeyNotFound(string entityName, int id)
			=> new KeyNotFoundException(string.Format(KeyNotFoundMsgTemplate, id));

		private ArgumentException IdsDontMatch()
			=> new ArgumentException(IdsDontMatchMsg);
	}
}