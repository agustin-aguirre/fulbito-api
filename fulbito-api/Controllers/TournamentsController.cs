using fulbito_api.Dtos.Tournaments;
using fulbito_api.Errors;
using fulbito_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace fulbito_api.Controllers
{
	[Route("fulbito-api/tournaments")]
	[ApiController]
	public class TournamentsController : ControllerBase
	{
		readonly ITournamentsService service;
		readonly int pageSize = 10;

		public TournamentsController(ITournamentsService service) => this.service = service;


		[HttpGet("{tournamentId:int}", Name = "GetTournament")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<TournamentDto>> GetTournament([FromRoute] int tournamentId)
		{
			var tournamentDto = await service.Get(tournamentId);
			if (tournamentDto == null) return NotFound();
			return Ok(tournamentDto);
		}


		[HttpGet]
		[ProducesResponseType(200)]
		public async Task<ActionResult<IEnumerable<TournamentDto>>> GetPage(int page)
			=> Ok(await service.GetPage(pageNumber: page, pageSize: pageSize));


		[HttpPost]
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


		[HttpPut("{tournamentId:int}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Update([FromRoute] int tournamentId, UpdateTournamentDto updateTournamentDto)
		{
			if (tournamentId != updateTournamentDto.Id) return BadRequest("Route Id and Json id don't match.");

			try
			{
				await service.Update(tournamentId, updateTournamentDto);
				return NoContent();
			}
			catch(EntityNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch(ArgumentOutOfRangeException ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpDelete("{tournamentId:int}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Delete([FromRoute] int tournamentId)
		{
			try
			{
				await service.Delete(tournamentId);
				return NoContent();
			}
			catch (EntityNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}
