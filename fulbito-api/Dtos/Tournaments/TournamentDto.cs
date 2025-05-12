namespace fulbito_api.Dtos.Tournaments
{
	public class TournamentDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public int TotalMatches { get; set; } = 1;
		public int PointsPerWonMatch { get; set; } = 3;
		public int PointsPerTiedMatch { get; set; } = 1;
		public int PointsPerLostMatch { get; set; } = 0;
		public int PointsPerAssertedMatch { get; set; } = 2;
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime LastModification { get; set; }
	}
}
