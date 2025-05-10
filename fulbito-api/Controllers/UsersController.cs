using fulbito_api.Dtos.Users;
using fulbito_api.Errors;
using fulbito_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace fulbito_api.Controllers
{
	[Route("fublito-api/users")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		readonly IUsersService<int> usersService;
		readonly int pageSize = 10;

		public UsersController(IUsersService<int> usersService) => this.usersService = usersService;


		[HttpGet("{userId:int}", Name = "GetUser")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<UserDto?>> GetUser([FromRoute] int userId)
		{
			var targetUser = await usersService.Get(userId);
			if (targetUser == null)
				return NotFound();
			return Ok(targetUser);
		}


		[HttpGet(Name = "GetPage")]
		[ProducesResponseType(200)]
		public async Task<ActionResult<IEnumerable<UserDto>>> GetPage(int page)
		{
			return Ok(await usersService.GetMany(pageNumber: page, pageSize: pageSize));
		}


		[HttpPost]
		public async Task<ActionResult> Post([FromBody] CreateUserDto createUserDto)
		{
			var newUserDto = await usersService.Create(createUserDto);
			return CreatedAtRoute("GetUser", new { UserId = newUserDto.Id }, newUserDto);
		}


		[HttpPut("{userId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Put(int userId, [FromBody] UpdateUserDto updatedUserDto)
		{
			try
			{
				await usersService.Update(userId, updatedUserDto);
				return NoContent();
			}
			catch (EntityNotFoundException)
			{
				return NotFound();
			}
		}


		[HttpDelete("{userId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Delete(int userId)
		{
			try
			{
				await usersService.Delete(userId);
				return NoContent();
			}
			catch (EntityNotFoundException)
			{
				return NotFound();
			}
		}
	}
}
