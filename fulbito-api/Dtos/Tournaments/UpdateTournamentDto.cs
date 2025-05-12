namespace fulbito_api.Dtos.Tournaments
{
	public class UpdateTournamentDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public int TotalMatches { get; set; }
		public int PointsPerWonMatch { get; set; }
		public int PointsPerTiedMatch { get; set; }
		public int PointsPerLostMatch { get; set; }
		public int PointsPerAssertedMatch { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
	}
}