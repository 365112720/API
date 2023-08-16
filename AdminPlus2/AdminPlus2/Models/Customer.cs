using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Customer
	{
		[Key]
			public int CustomerID { get; set; }
		    public string? Account { get; set; }

		    public string? Password	{ get; set; }

		    public string? Telphone{ get; set; }

		    public string? Email { get; set; }
		    public DateTime? CreateTime { get; set; }

	}
}
