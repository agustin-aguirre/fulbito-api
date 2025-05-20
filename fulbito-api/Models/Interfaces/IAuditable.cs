namespace fulbito_api.Models.Interfaces
{
	public interface IAuditable
	{
		DateTime CreationDate { get; set; }
		DateTime LastModification { get; set; }
	}
}
