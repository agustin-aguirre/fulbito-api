using fulbito_api.Data;
using fulbito_api.Errors;
using fulbito_api.Models;
using fulbito_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace fulbito_api.Repositories
{
	public class UsersRepo : IUsersRepo<int>
	{
		readonly AppDbContext dbContext;


		public UsersRepo(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}


		public async Task<User?> Get(int id) => await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

		public async Task<IEnumerable<User>> Get(int pageNumber, int pageSize) 
			=> await dbContext.Users
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

		public async Task<User> Add(User newUser)
		{
			newUser.CreationDate = DateTime.Now;
			await dbContext.Users.AddAsync(newUser);
			await persist();
			return newUser;
		}

		public async Task Update(int id, User updatedUserData)
		{
			var targetUser = await Get(id);
			if (targetUser == null)
				throw new EntityNotFoundException($"User with id:{updatedUserData.Id} does not exist.");
			targetUser.Name = updatedUserData.Name;
			await persist();
		}

		public async Task Delete(int id)
		{
			var targetUser = await Get(id);
			if (targetUser == null)
				throw new EntityNotFoundException($"User with id:{id} does not exist.");
			dbContext.Users.Remove(targetUser);
			await persist();
		}


		async Task persist()
		{
			await dbContext.SaveChangesAsync();
		}
	}
}
