using System.ComponentModel.DataAnnotations;

namespace Models
{
	public class User
	{

			[Key]
			public int UserID { get; set; }
			public string? Username { get; set; }
			public string? Password { get; set; }
			public DateTime? CreateTime { get; set; }

		}

	}
