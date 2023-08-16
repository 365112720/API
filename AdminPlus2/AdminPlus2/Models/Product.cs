using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		public string? ProductName { get; set; }

		public string? ProductDescription { get; set; }

		public int Postage { get; set; }

		public DateTime? CreateTime { get; set; }
	}
}
