using fulbito_api.Data;
using fulbito_api.Errors;
using fulbito_api.Models;
using fulbito_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace fulbito_api.Repositories
{
	public class TournamentsRepo : ITournamentsRepo<int>
	{
		readonly AppDbContext dbContext;

		public TournamentsRepo(AppDbContext dbContext) => this.dbContext = dbContext;


		public async Task<Tournament?> Get(int id)
			=> await dbContext.Tournaments.FirstOrDefaultAsync(t => t.Id == id);


		public async Task<IEnumerable<Tournament>> GetMany(int pageNumber, int pageSize)
			=> await dbContext.Tournaments
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();


		public async Task<bool> Exists(int id)
			=> await dbContext.Tournaments.AnyAsync(t => t.Id == id);


		public async Task<Tournament> Create(Tournament newTournamentData)
		{
			newTournamentData.CreationDate = DateTime.Now;
			newTournamentData.LastModification = DateTime.Now;

			await dbContext.Tournaments.AddAsync(newTournamentData);
			await persist();

			return newTournamentData;
		}


		public async Task Update(int id, Tournament updatedTournamentData)
		{
			if (id != updatedTournamentData.Id)
				throw new ArgumentException("Passed Id and UpdatedTournamentData.Id don't match.");

			if (! await Exists(id)) throw newNotFound(id);

			updatedTournamentData.LastModification = DateTime.Now;
			
			dbContext.Tournaments.Update(updatedTournamentData);
			await persist();
		}


		public async Task Delete(int id)
		{
			var targetTournament = await Get(id);

			if (targetTournament == null) throw newNotFound(id);
			
			dbContext.Tournaments.Remove(targetTournament);
			await persist();
		}


		EntityNotFoundException newNotFound(int id)
			=> new EntityNotFoundException($"Tournament with id:{id} does not exist.");


		async Task persist()
		{
			await dbContext.SaveChangesAsync();
		}
	}
}
