using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace fulbito_api.Models
{
	public class PlayerStatsFromTournament
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("Tournament")]
		public int TournamentId { get; set; }

		[ForeignKey("User")]
		public int UserId { get; set; }

		public int TotalPoints { get; set; } = 0;
		public int GoalCount { get; set; } = 0;
		public int AssitenciesCount { get; set; } = 0;
		public int PlayedMatchesCount { get; set; } = 0;
		public int WonMatchesCount { get; set; } = 0;
		public int TiedMatchesCount { get; set; } = 0;
		public int LostMatchesCount { get; set; } = 0;
		public int AssertedMatchesCount { get; set; } = 0;

		[Required]
		public DateTime CreationDate { get; set; } = DateTime.Now;
		[Required]
		public DateTime LastModification { get; set; }
	}
}
