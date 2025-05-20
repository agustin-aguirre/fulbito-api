using fulbito_api.Data;
using fulbito_api.Models;
using fulbito_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace fulbito_api.Repositories
{
	public class TournamentsRepo : ITournamentsRepo
	{
		readonly AppDbContext dbContext;

		public TournamentsRepo(AppDbContext dbContext) => this.dbContext = dbContext;


		public async Task<Tournament> Create(Tournament newTournament)
		{
			await dbContext.Tournaments.AddAsync(newTournament);
			await persistChanges();
			return newTournament;
		}

		public async Task<bool> Delete(int tournamentId)
		{
			int affectedRows = await dbContext.Tournaments
					.Where(t => t.Id == tournamentId)
					.ExecuteDeleteAsync();
			return affectedRows > 0;
		}

		public async Task<bool> Exists(int tournamentId) 
			// return: tournament is in cache || tournament is in db
			=> (await getFromCache(tournamentId)) is not null || await dbContext.Tournaments.AnyAsync(t => t.Id == tournamentId);

		public async Task<Tournament?> Get(int tournamentId)
			// Primero busca a la entidad en la caché de EF.
			// Si la entidad está siendo trackeada, retorna eso.
			// Sino, realiza la consulta a la base de datos.
			=> await getFromCache(tournamentId) ?? await getFromDB(tournamentId);

		public async Task<IEnumerable<Tournament>> GetMany(IEnumerable<int> tournamentIds) 
			=> await dbContext.Tournaments.Where(t => tournamentIds.Contains(t.Id)).ToListAsync();

		public async Task<IEnumerable<Tournament>> GetPage(int pageNumber, int pageSize)
			=> await dbContext.Tournaments.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();

		public async Task Update(Tournament updatedTournament)
		{
			// fuerza cachear la entidad con Get (la encuentra en caché o la carga de BBDD)
			var cachedTournament = await Get(updatedTournament.Id);
			if (cachedTournament is not null)
				dbContext.Entry(cachedTournament).CurrentValues.SetValues(updatedTournament);
			await persistChanges();
		}


		async Task<Tournament?> getFromCache(int id) => await dbContext.Tournaments.FindAsync(id);
		async Task<Tournament?> getFromDB(int id) => await dbContext.Tournaments.FirstOrDefaultAsync(t => t.Id == id);
		async Task persistChanges() => await dbContext.SaveChangesAsync();
	}
}