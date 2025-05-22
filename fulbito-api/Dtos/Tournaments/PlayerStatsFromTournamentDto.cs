using fulbito_api.Models.Interfaces;
using System.ComponentModel.DataAnnotations;


namespace fulbito_api.Dtos.Tournaments
{
	public class PlayerStatsFromTournamentDto : IAuditable
	{
		public int TournamentId { get; set; }
		public int UserId { get; set; }
		public int GoalCount { get; set; } = 0;
		public int AssitenciesCount { get; set; } = 0;
		public int PlayedMatchesCount { get; set; } = 0;
		public int WonMatchesCount { get; set; } = 0;
		public int TiedMatchesCount { get; set; } = 0;
		public int LostMatchesCount { get; set; } = 0;
		public int AssertedMatchesCount { get; set; } = 0;
		[Required]
		public DateTime CreationDate { get; set; }
		[Required]
		public DateTime LastModification { get; set; }
	}
}
