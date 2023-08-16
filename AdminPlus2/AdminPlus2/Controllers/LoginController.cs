using AdminPlus2.Models;
using AdminPlus2.Utility;
using AdminXP.Utility.SwaggerExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using ShopInterface;

namespace AdminPlus2.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]

	[ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersions.用户前台管理))]  //显示效果
	public class LoginController : ControllerBase
	{

		private readonly ILogger<LoginController> _logger;

		private readonly DbContext _context;

		private readonly ICustomerService  _customerService;

		public LoginController(ILogger<LoginController> logger, DbContext context, ICustomerService customerService)
		{
			_logger = logger;


			_customerService = customerService;
		}
		#region 登录
		/// <summary>
		/// 用户登录
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		[HttpGet]
		public ResultModel Login(string username, string password) {

		ResultModel result = new ResultModel();
			try
			{
				Customer customer = _customerService.Query<Customer>(X => X.Account == username && X.Password == password).First();
				result.msg = "登录成功";
				result.data = customer;
				
			}
			catch (Exception ex)
			{
				result.code = (int)ResultCode.EEEOR;
				result.msg = "用户名或密码错误";
				
			}
		
		return result;
		}
		#endregion

		#region 获取所有用户
		/// <summary>
		/// 获取所有用户
		/// </summary>
		/// <returns></returns>
		//[HttpGet]

		[HttpGet]
		public ResultModel GetAllCustomer()
		{
			ResultModel result = new ResultModel();
			try
			{
				//查询所有
				result.data = _customerService.Set<Customer>();
				result.msg = "获取成功";
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

		#region 获取用户ID
		/// <summary>
		/// 根据ID获取用户
		/// </summary>
		/// <returns></returns>
		//[HttpGet]

		[HttpGet]
		public ResultModel GetCustomerForId(int CustomerId)
		{
			ResultModel result = new ResultModel();
			try
			{
				if(CustomerId >0)
				{
				


					//根据ID查询
					Customer customer= _customerService.Find<Customer>(CustomerId);

					if (customer == null)
					{
					//result.data = customer;
					result.msg = "用户不存在";
					}
					else
					{
						result.data = customer;
						result.msg = "获取成功";
					}
					//result.data = _customerService.Find<Customer>(CustomerId); //直接返回表达式

				}
				else
				{
					result.msg = "用户存在";
				}
				
			
			
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


		#region 获取用户名
		/// <summary>
		/// 根据用户名获取用户
		/// </summary>
		/// <returns></returns>
		//[HttpGet]

		[HttpGet]
		public ResultModel GetCustomerForAccount(string Account)
		{
			ResultModel result = new ResultModel();
			try
			{
				if(Account != null)
				{
					Customer customer = _customerService.Query<Customer>(P1 => P1.Account.Contains(Account)).FirstOrDefault();
					if(customer == null)
					{
						result.msg = "用户名不存在";
					}
					else
					{
						result.data= customer;
						result.msg = "获取成功";
					}
				}
				else
				{
					result.msg = "用户名存在";
				}
			
				//根据用户名查询
				//result.data = _customerService.Query<Customer>(P1 => P1.Account.Contains(Account));
				//result.msg = "用户不存在";
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

		#region 修改
		/// <summary>
		/// 修改用户
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ResultModel UpdateCustomer(Customer updateCustomer)
		{
			ResultModel result = new ResultModel();
			try
			{
				//判断数据是否为空
				if (CustomerHelps.CheckCustomer(updateCustomer))
				{
					result.code = (int)ResultCode.EEEOR;
					result.msg = "用户名或密码为空";
					return result;
				}
				//判断用户是否存在
				//1.根据用户id查询数据库
				Customer customer = _customerService.Find<Customer>(updateCustomer.CustomerID);
				//2.判断数据库是否存在该用户
				if (customer != null)
				{
					//3.修改用户信息---这里只修改用户名和密码
					customer.Account = updateCustomer.Account;
					customer.Password = updateCustomer.Password;
					customer.Telphone = updateCustomer.Telphone;
					customer.Email = updateCustomer.Email;
					_customerService.Update<Customer>(customer);

					result.data = customer;//返回结果
				}
				else
				{
					result.code = (int)ResultCode.EEEOR;
					result.msg = "用户不存在";
					return result;
				}
			}
			catch (Exception ex)
			{
				//出现异常
				result.code = (int)ResultCode.EEEOR;
				result.msg = ex.Message;
			}
			
			result.msg = "修改成功";
			return result;
		}
		#endregion

		#region 添加方法
		/// <summary>
		/// 添加用户
		/// </summary>
		/// <returns></returns>
		[HttpPut]
		public ResultModel AddCustomer(Customer newCustomer)
		{
			ResultModel result = new ResultModel();
			try
			{
				//1.判断用户名和密码是否为空-----判断密码长度等也是在这里判断
				if (CustomerHelps.CheckCustomer(newCustomer))
				{
					result.code = (int)ResultCode.EEEOR;
					result.msg = "用户名或密码为空";
					return result;
				}
				//2.根据用户名查询数据库
				Customer customer  = _customerService.Query<Customer>(P1 => P1.Account.Equals(newCustomer.Account)).FirstOrDefault();
				//3.判断数据库是否存在相同用户名的用户
				if (customer != null)
				{
					//已经存在相同用户名的用户
					result.code = (int)ResultCode.EEEOR;
					result.msg = "已经存在相同用户名的用户";
					return result;
				}
				//4.保存到数据库
				_customerService.Insert<Customer>(newCustomer);
				result.data = newCustomer; //返回结果
			}
			catch (Exception ex)
			{
				//出现异常
				result.code = (int)ResultCode.EEEOR;
				result.msg = ex.Message;
			}
			
			result.msg = "添加成功";
			return result;
		}
		#endregion

		#region 删除
		/// <summary>
		/// 删除用户
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpDelete]
		public ResultModel DeleteCustomer(int CustomerId)
		{

			ResultModel result = new ResultModel();
			try
			{
				//判断用户是否存在
				Customer customer   = _customerService.Find<Customer>(CustomerId);
				if (customer == null)
				{
					result.code = (int)ResultCode.EEEOR;
					result.msg = "用户已经删除";
					return result;
				}
				//根据用户ID删除
				_customerService.Delete<Customer>(CustomerId);
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

	}
}
