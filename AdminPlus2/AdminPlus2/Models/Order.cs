using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Order
	{
		[Key]
		public int OrderID { get; set; }
		public string? OrderState { get; set; }
		public decimal? OrderMoney { get; set; }
		public string? SenDate { get; set; }
		public string? RecevieDate { get; set; }
		public string? AddressInfo { get; set; }
		public string? InvoiceName { get; set; }
		public string? InvoiceType { get; set; }
		public decimal? Postage { get; set; }
		public string? Express { get; set; }
		public string? ExpressNumber { get; set; }
		public DateTime? CreateTime { get; set; }

		public int CustomerID { get; set;}
	}
}
