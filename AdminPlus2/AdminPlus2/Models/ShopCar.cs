using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class ShopCar
	{
		[Key]
		public int Id { get; set; }

		public int CustomerID { get; set; }

		public int ProperID { get; set; }

		public int Quantity { get; set; }

		public DateTime? CreateTime { get; set; }
	}
}
