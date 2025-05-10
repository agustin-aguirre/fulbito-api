namespace fulbito_api.Dtos.Tournaments
{
	public class TournamentPlayerStatsDto
	{
		public int TournamentId { get; set; }
		public int UserId { get; set; }
		public int Position { get; set; }
		public int GoalCount { get; set; } = 0;
		public int AssitenciesCount { get; set; } = 0;
		public int PlayedMatchesCount { get; set; } = 0;
		public int WonMatchesCount { get; set; } = 0;
		public int TiedMatchesCount { get; set; } = 0;
		public int LostMatchesCount { get; set; } = 0;
		public int AssertedMatchesCount { get; set; } = 0;
		public DateTime CreationDate { get; set; }
		public DateTime LastModification { get; set; }
	}
}
