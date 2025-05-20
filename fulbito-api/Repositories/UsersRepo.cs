using fulbito_api.Data;
using fulbito_api.Models;
using fulbito_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace fulbito_api.Repositories
{
	public class UsersRepo : IUsersRepo
	{
		readonly AppDbContext dbContext;


		public UsersRepo(AppDbContext dbContext) => this.dbContext = dbContext;


		public async Task<User> Create(User newUser)
		{
			await dbContext.Users.AddAsync(newUser);
			await persistChanges();
			return newUser;
		}

		public async Task<bool> Delete(int userId)
		{
			int affectedRows = await dbContext.Users
					.Where(u => u.Id == userId)
					.ExecuteDeleteAsync();
			return affectedRows > 0;
		}

		public async Task<bool> Exists(int userId) 
			// return user is in cache || user is in db
			=> (await getFromCache(userId)) is not null || await dbContext.Users.AnyAsync(u => u.Id == userId);

		public async Task<User?> Get(int userId) 
			// Primero busca a la entidad en la caché de EF.
			// Si la entidad está siendo trackeada, retorna eso.
			// Sino, realiza la consulta a la base de datos.
			=> await getFromCache(userId) ?? await getFromDB(userId);

		public async Task<IEnumerable<User>> GetMany(IEnumerable<int> userIds) 
			=> await dbContext.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();

		public async Task Update(User updatedUser)
		{
			// fuerza cachear la entidad con Get (la encuentra en caché o la carga de BBDD)
			var cachedUser = await Get(updatedUser.Id);
			if (cachedUser is not null)
				dbContext.Entry(cachedUser).CurrentValues.SetValues(updatedUser);
			await persistChanges();
		}


		async Task<User?> getFromCache(int id) => await dbContext.Users.FindAsync(id);
		async Task<User?> getFromDB(int id) => await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
		async Task persistChanges() => await dbContext.SaveChangesAsync();
	}
}
