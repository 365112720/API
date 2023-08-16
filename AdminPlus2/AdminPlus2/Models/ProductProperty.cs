using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class ProductProperty
	{
		[Key]
		public int ProperID { get; set; }

		public string? ProperName { get; set; }

		public string? Image { get; set;}

		public  decimal  Price { get; set; }

		public int Quantity { get; set;}

		public int TypeID { get; set; }

		public string? Description { get; set; }     
	    public DateTime? CreateTime { get; set; }


	}
}
