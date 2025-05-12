using fulbito_api.Dtos.Tournaments;
using fulbito_api.Models;

namespace fulbito_api.Services.Helpers
{
	public class TournamentPointsHelper
	{
		public int? PointsPerWonMatch { get; set; }
		public int? PointsPerTiedMatch { get; set; }
		public int? PointsPerLostMatch { get; set; }
		public int? PointsPerAssertedMatch { get; set; }


		public TournamentPointsHelper() { }
		public TournamentPointsHelper(Tournament tournament)
		{
			PointsPerWonMatch = tournament.PointsPerWonMatch;
			PointsPerTiedMatch = tournament.PointsPerTiedMatch;
			PointsPerLostMatch = tournament.PointsPerLostMatch;
			PointsPerAssertedMatch = tournament.PointsPerAssertedMatch;
		}
		public TournamentPointsHelper(TournamentDto tournamentDto)
		{
			PointsPerWonMatch = tournamentDto.PointsPerWonMatch;
			PointsPerTiedMatch = tournamentDto.PointsPerTiedMatch;
			PointsPerLostMatch = tournamentDto.PointsPerLostMatch;
			PointsPerAssertedMatch = tournamentDto.PointsPerAssertedMatch;
		}
		public TournamentPointsHelper(CreateTournamentDto createTournamentDto)
		{
			PointsPerWonMatch = createTournamentDto.PointsPerWonMatch;
			PointsPerTiedMatch = createTournamentDto.PointsPerTiedMatch;
			PointsPerLostMatch = createTournamentDto.PointsPerLostMatch;
			PointsPerAssertedMatch = createTournamentDto.PointsPerAssertedMatch;
		}
		public TournamentPointsHelper(UpdateTournamentDto updateTournamentDto)
		{
			PointsPerWonMatch = updateTournamentDto.PointsPerWonMatch;
			PointsPerTiedMatch = updateTournamentDto.PointsPerTiedMatch;
			PointsPerLostMatch = updateTournamentDto.PointsPerLostMatch;
			PointsPerAssertedMatch = updateTournamentDto.PointsPerAssertedMatch;
		}
	}
}
