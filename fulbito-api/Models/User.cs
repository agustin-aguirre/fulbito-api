using fulbito_api.Models.Interfaces;
using System.ComponentModel.DataAnnotations;


namespace fulbito_api.Models
{
	public class User : IAuditable
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		[MaxLength(64)]
		public string Name { get; set; } = string.Empty;

		[Required]
		public DateTime CreationDate { get; set; }
		[Required]
		public DateTime LastModification { get; set; }
	}
}
