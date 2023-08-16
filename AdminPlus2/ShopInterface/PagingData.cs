using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopInterface
{/// <summary>
/// 查询分页
/// </summary>
/// <typeparam name="T"></typeparam>
   public	 class PagingData<T> where T : class
	{

		public int Count { get; set; }

		public int PageIndex { get; set; }

		public int pageSize { get; set; }

		public List<T>?  DATAlist { get; set; }

		public string? Search {get; set;}
	}
}
