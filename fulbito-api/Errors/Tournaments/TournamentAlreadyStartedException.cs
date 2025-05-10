namespace fulbito_api.Errors.Tournaments
{
	public class TournamentAlreadyStartedException : Exception
	{
		public TournamentAlreadyStartedException(string? message) : base(message)
		{
		}
	}
}
