using System.ComponentModel.DataAnnotations;


namespace fulbito_api.Models
{	
	public class Tournament
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(128)]
		public string Name { get; set; } = string.Empty;
		
		public int PlayedMatchesCount { get; set; } = 0;
		
		// Config
		public int TotalMatches { get; set; } = 1;
		public int PointsPerWonMatch { get; set; } = 3;
		public int PointsPerTiedMatch { get; set; } = 1;
		public int PointsPerLostMatch { get; set; } = 0;
		public int PointsPerAssertedMatch { get; set; } = 2;
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		[Required]
		public DateTime CreationDate { get; set; }
		[Required]
		public DateTime LastModification { get; set; }
	}
}
