using fulbito_api.Dtos.Users;


namespace fulbito_api.Services.Interfaces
{
	public interface IUsersService
	{
		Task<UserDto> Create(CreateUserDto createUserDto);
		Task Delete(int userId);
		Task<UserDto?> GetById(int id);
		Task<IEnumerable<UserDto>> GetManyById(IEnumerable<int> ids);
		Task Update(int userId, UpdateUserDto updateUserDto);
	}
}