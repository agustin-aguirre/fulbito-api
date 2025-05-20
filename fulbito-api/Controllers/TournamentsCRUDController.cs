using fulbito_api.Dtos.Tournaments;
using fulbito_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace fulbito_api.Controllers
{
	[Route("fulbito-api/tournaments")]
	[ApiController]
	public class TournamentsCRUDController : ControllerBase
	{
		readonly ITournamentsService service;
		
		public TournamentsCRUDController(ITournamentsService service) => this.service = service;


		[HttpGet("{tournamentId:int}", Name = "GetTournament")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<TournamentDto>> GetTournament([FromRoute] int tournamentId)
		{
			try
			{
				return Ok(await service.GetById(tournamentId));
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}


		[HttpGet(Name = "GetPage")]
		[ProducesResponseType(200)]
		public async Task<ActionResult<IEnumerable<TournamentDto>>> GetPage([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
			=> Ok(await service.GetPage(pageNumber.GetValueOrDefault(), pageSize.GetValueOrDefault()));


		[HttpPost(Name = "CreateTournament")]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public async Task<ActionResult> Create([FromBody] CreateTournamentDto createTournamentDto)
		{
			try
			{
				var newTournamentDto = await service.Create(createTournamentDto);
				return CreatedAtAction("GetTournament", new { TournamentId = newTournamentDto.Id }, newTournamentDto);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpPut("{tournamentId:int}", Name = "PutTournament")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Put([FromRoute] int tournamentId, UpdateTournamentDto updateTournamentDto)
		{
			try
			{
				await service.Update(tournamentId, updateTournamentDto);
				return NoContent();
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpDelete("{tournamentId:int}", Name = "DeleteTournament")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Delete([FromRoute] int tournamentId)
		{
			try
			{
				await service.Delete(tournamentId);
				return NoContent();
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}
