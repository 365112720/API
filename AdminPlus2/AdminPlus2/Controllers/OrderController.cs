using AdminPlus2.Models;
using AdminPlus2.Utility;
using AdminXP.Utility.SwaggerExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using ShopInterface;
using ShopService;

namespace AdminPlus2.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]

	[ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersions.订单信息管理))]  //显示效果
	public class OrderController : ControllerBase
	{
		private readonly ILogger<OrderController> _logger;

		private readonly DbContext _context;

		private readonly IOrderService _orderService;

		public OrderController(ILogger<OrderController> logger, DbContext context, IOrderService orderService)
		{
			_logger = logger;


			_orderService = orderService;
		}
		//[HttpGet]	
		//public ActionResult Order (Order order) { 

		//return Ok(order);
		//}

        #region 获取所有用户订单
        /// <summary>
        /// 获取所有用户订单
        /// </summary>
        /// <returns></returns>
        //[HttpGet]

        [HttpGet]
        public ResultModel GetAllOrder()
        {
            ResultModel result = new ResultModel();
            try
            {
                //查询所有
                result.data = _orderService.Set<Order>();
                result.msg = "获取订单成功";
            }
            catch (Exception ex)
            {
                //出现异常
                result.code = (int)ResultCode.EEEOR;
                result.msg = ex.Message;
            }
            return result;
        }
        #endregion


        #region 添加用户订单
        /// <summary>
        /// 添加用户订单
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public ResultModel AddOrder(Order newOrder)
        {
            ResultModel result = new ResultModel();
            try
            {
                //1.判断是否为空-----判断长度等也是在这里判断
                if (OrderHelps.CheckOrder(newOrder))
                {
                    result.code = (int)ResultCode.EEEOR;
                    result.msg = "订单数据为空";
                    return result;
                }
                //2.根据用户名查询数据库
                Order order = _orderService.Query<Order>(P1 => P1.CustomerID.Equals(newOrder.CustomerID)).FirstOrDefault();
                //3.判断数据库是否存在相同用户名的用户
                if (order != null)
                {
                    //已经存在相同用户名的用户
                    result.code = (int)ResultCode.EEEOR;
                    result.msg = "已经存在相同用户名订单用户";
                    return result;
                }
                //4.保存到数据库
                _orderService.Insert<Order>(newOrder);
                result.data = newOrder; //返回结果
            }
            catch (Exception ex)
            {
                //出现异常
                result.code = (int)ResultCode.EEEOR;
                result.msg = ex.Message;
            }

            result.msg = "添加订单成功";
            return result;
        }
        #endregion


    }
}
