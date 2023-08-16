using Microsoft.EntityFrameworkCore;
using ShopInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopService
{
    //使用公共方法  继承 baes  继承 接口   //右键重构  生成构造函数 contest 方法
    public class ProductPropertyService : BaseService, IProductPropertyService
    {
        public ProductPropertyService(DbContext context) : base(context)
        {
        }
    }
}
