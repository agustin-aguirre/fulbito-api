namespace fulbito_api.Errors
{
	public class EntityNotFoundException : Exception
	{
		public EntityNotFoundException(string? message) : base(message)
		{
		}
	}
}
