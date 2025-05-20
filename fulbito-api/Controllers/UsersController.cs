using fulbito_api.Dtos.Users;
using fulbito_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace fulbito_api.Controllers
{
	[Route("fublito-api/users")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		readonly IUsersService usersService;

		public UsersController(IUsersService usersService) => this.usersService = usersService;


		[HttpGet("{userId:int}", Name = "GetUser")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<UserDto?>> GetUser([FromRoute] int userId)
		{
			try
			{
				return Ok(await usersService.GetById(userId));
			}
			catch(KeyNotFoundException knfe)
			{
				return NotFound(knfe.Message);
			}
		}


		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Post([FromBody] CreateUserDto createUserDto)
		{
			try
			{
				var newUserDto = await usersService.Create(createUserDto);
				return CreatedAtRoute("GetUser", new { UserId = newUserDto.Id }, newUserDto);
			}
			catch(ArgumentException ae)
			{
				return BadRequest(ae.Message);
			}
			catch(KeyNotFoundException knfe)
			{
				return NotFound(knfe.Message);
			}
		}


		[HttpPut("{userId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Put(int userId, [FromBody] UpdateUserDto updatedUserDto)
		{
			try
			{
				await usersService.Update(userId, updatedUserDto);
				return NoContent();
			}
			catch (ArgumentException ae)
			{
				return BadRequest(ae.Message);
			}
			catch (KeyNotFoundException knfe)
			{
				return NotFound(knfe.Message);
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
			catch (KeyNotFoundException knfe)
			{
				return NotFound(knfe.Message);
			}
		}
	}
}
