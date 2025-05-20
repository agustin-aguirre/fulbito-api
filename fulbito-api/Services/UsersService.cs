using AutoMapper;
using fulbito_api.Dtos.Users;
using fulbito_api.Models;
using fulbito_api.Repositories.Interfaces;
using fulbito_api.Sanitizers.Interfaces;
using fulbito_api.Services.Interfaces;


namespace fulbito_api.Services
{
	public class UsersService : IUsersService
	{
		readonly IUsersRepo repo;
		readonly IMapper mapper;
		readonly ISanitizer sanitizer;

		const int NameMaxLength = 24;

		const string KeyNotFoundMsgTemplate = "User with id:{0} does not exist.";
		const string NameTooShortMsg = "User name cannot be null or empty.";
		const string NameTooLongMsgTemplate = "User name is too long. Max length is {0}";
		const string IdsDontMatchMsg = "Passed id and user id don't match.";


		public UsersService(IUsersRepo repo, IMapper mapper, ISanitizer sanitizer)
		{
			this.repo = repo;
			this.mapper = mapper;
			this.sanitizer = sanitizer;
		}


		public async Task<UserDto?> GetById(int id)
		{
			if (id <= 0) throw KeyNotFound(id);

			var user = await repo.Get(id);
			if (user is null) throw KeyNotFound(id);

			return mapper.Map<UserDto>(user);
		}

		public async Task<IEnumerable<UserDto>> GetManyById(IEnumerable<int> ids)
			=> mapper.Map<IEnumerable<UserDto>>(await repo.GetMany(ids));

		public async Task<UserDto> Create(CreateUserDto createDto)
		{
			createDto.Name = sanitizer.Sanitize(createDto.Name);
			
			checkName(createDto.Name);

			var newUser = mapper.Map<User>(createDto);
			newUser = await repo.Create(newUser);
			
			return mapper.Map<UserDto>(newUser);
		}

		public async Task Delete(int id)
		{
			if (id <= 0 || !await repo.Exists(id)) throw KeyNotFound(id);
			await repo.Delete(id);
		}

		public async Task Update(int id, UpdateUserDto updateDto)
		{
			if (id != updateDto.Id) throw IdsDontMatch();

			if (id <= 0) throw KeyNotFound(id);
			
			updateDto.Name = sanitizer.Sanitize(updateDto.Name);

			checkName(updateDto.Name);

			if (!await repo.Exists(id)) throw KeyNotFound(id);

			var updatedUser = mapper.Map<User>(updateDto);
			await repo.Update(updatedUser);
		}


		private void checkName(string name)
		{
			if (string.IsNullOrEmpty(name)) throw NameTooShort();
			if (name.Length > NameMaxLength) throw NameTooLong();
		}

		private KeyNotFoundException KeyNotFound(int id)
			=> new KeyNotFoundException(string.Format(KeyNotFoundMsgTemplate, id));

		private ArgumentNullException NameTooShort()
			=> new ArgumentNullException(NameTooShortMsg);

		private ArgumentOutOfRangeException NameTooLong()
			=> new ArgumentOutOfRangeException(string.Format(NameTooLongMsgTemplate, NameMaxLength));

		private ArgumentException IdsDontMatch()
			=> new ArgumentException(IdsDontMatchMsg);
	}
}
