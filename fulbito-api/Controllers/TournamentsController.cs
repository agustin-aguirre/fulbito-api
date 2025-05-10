using fulbito_api.Dtos.Tournaments;
using Microsoft.AspNetCore.Mvc;


namespace fulbito_api.Controllers
{
	[Route("fulbito-api/tournaments")]
	[ApiController]
	public class TournamentsController : ControllerBase
	{
		readonly int pageSize = 10;


		[HttpGet("{tournamentId:int}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<TournamentDto>> Get(int tournamentId)
		{
			throw new NotImplementedException();
		}


		[HttpGet]
		[ProducesResponseType(200)]
		public async Task<ActionResult<IEnumerable<TournamentDto>>> GetPage(int pageNumber)
		{
			throw new NotImplementedException();
		}


		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public async Task<ActionResult> Create([FromBody] CreateTournamentDto createTournamentDto)
		{
			throw new NotImplementedException();
		}


		[HttpDelete]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Delete(int id)
		{
			throw new NotImplementedException();
		}
	}
}
