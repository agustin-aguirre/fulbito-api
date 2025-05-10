using fulbito_api.Dtos.Users;


namespace fulbito_api.Services.Interfaces
{
	public interface IUsersService<TId>
	{
		Task<UserDto> Create(CreateUserDto createUserDto);
		Task Delete(TId id);
		Task<UserDto?> Get(TId id);
		Task<IEnumerable<UserDto>> GetMany(int pageNumber, int pageSize);
		Task Update(TId id, UpdateUserDto updateUserDto);
	}
}