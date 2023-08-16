using Microsoft.EntityFrameworkCore;
using ShopInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService
{
	public class CustomerService : BaseService, ICustomerService
	{
		public CustomerService(DbContext context) : base(context)
		{
		}
	}
}
