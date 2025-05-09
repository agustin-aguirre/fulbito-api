using fulbito_api.Models;

namespace fulbito_api.Repositories.Interfaces
{
	public interface IUsersRepo<IdType>
	{
		Task<User> Add(User newUser);
		Task<User?> Get(IdType id);
		Task<IEnumerable<User>> Get(int pageNumber, int pageSize);
		Task Delete(IdType id);
		Task Update(IdType id, User updatedUserData);
	}
}