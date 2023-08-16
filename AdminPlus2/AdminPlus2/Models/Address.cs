using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Address
	{
		[Key]
		public int AddressID { get; set; }
		public string? Areas { get; set; }
		
		public string? AddressInfo { get; set; }

		public byte IsDefault{ get; set; }

		public string? Tel { get; set; }

		public string? Postalcode{ get; set; }

		public string? AddressType { get; set; }

		public string? Phone { get; set; }

		public DateTime? CreateTime { get; set; }
		
		public int CustomerID { get; set; }

		public string? AddressUser { get; set; }



	}
}
