using AutoMapper;
using fulbito_api.Dtos.Users;
using fulbito_api.Models;
using fulbito_api.Repositories.Interfaces;
using fulbito_api.Services.Interfaces;


namespace fulbito_api.Services
{
	public class UsersService : IUsersService<int>
	{
		readonly IUsersRepo<int> repo;
		readonly IMapper mapper;

		public UsersService(IUsersRepo<int> repo, IMapper userMapper)
		{
			this.repo = repo;
			this.mapper = userMapper;
		}


		public async Task<UserDto?> Get(int id)
		{
			var user = await repo.Get(id);
			if (user == null) return null;
			return mapper.Map<UserDto>(user);
		}

		public async Task<IEnumerable<UserDto>> GetMany(int pageNumber, int pageSize)
		{
			var users = await repo.Get(pageNumber, pageSize);
			var userDtos = mapper.Map<List<UserDto>>(users);
			return userDtos;
		}

		public async Task<UserDto> Create(CreateUserDto createUserDto)
		{
			var newUser = mapper.Map<User>(createUserDto);
			await repo.Add(newUser);
			var newUserDto = mapper.Map<UserDto>(newUser);
			return newUserDto;
		}

		public async Task Update(int id, UpdateUserDto updatedUserDto)
		{
			var updatedUser = mapper.Map<User>(updatedUserDto);
			await repo.Update(id, updatedUser);
			return;
		}

		public async Task Delete(int id)
		{
			await repo.Delete(id);
			return;
		}
	}
}
