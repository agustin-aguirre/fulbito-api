namespace fulbito_api.Dtos.Tournaments
{
	public class TournamentPositionsDto
	{
		public required TournamentDto Tournament { get; set; }
		public ICollection<PlayerStatsFromTournamentDto> Positions { get; set; } = new List<PlayerStatsFromTournamentDto>();
	}
}
