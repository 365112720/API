using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class OrderDeatils
	{
		[Key]
		public int DetialID { get; set; }

		public int OrderID { get; set; }

		public int ProperID { get; set; }

		public string? ProductName { get; set; }

		public string? TypeName { get; set; }

		public string? ProperName { get; set; }

		public string? Image { get; set;}

		public int Quantity { get; set; }

		public decimal Price { get; set;}

		public decimal PriceTotalMoney { get; set;}

		public DateTime? CreateTime { get; set; }


	}
}
