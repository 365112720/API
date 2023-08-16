using AdminPlus2.Models;
using AdminPlus2.Utility;
using AdminXP.Utility.SwaggerExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using ShopInterface;
using System.Linq;

namespace AdminPlus2.Controllers
{
	/// <summary>
	/// 用户
	/// </summary>
    /// 
	[Route("api/[controller]/[action]")]

   // api/[controller][/action]
    [ApiController]

	[ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersions.用户后台管理))]  //显示效果
	public class UserController : ControllerBase
	{

		private readonly ILogger<UserController> _logger;

		private readonly DbContext _context;

		private readonly IUserService _user;


		public UserController(ILogger<UserController> logger, DbContext context, IUserService user)
		{
			_logger = logger;
			_user = user;
		}
		/// <summary>
		/// 获取所有用户
		/// </summary>
		/// <returns></returns>
		//[HttpGet]

		[HttpGet]
		public ResultModel GetAllUser()
		{
            ResultModel result = new ResultModel();
            try
            {
                //查询所有
                result.data = _user.Set<User>();
            }
			catch (Exception ex)
			{
				//出现异常
				result.code = (int)ResultCode.EEEOR;
                result.msg=ex.Message;
            }
			return result;
        }
        /// <summary>
        /// 根据ID获取用户
        /// </summary>
        /// <returns></returns>
        //[HttpGet]

        [HttpGet]
        public ResultModel GetUserForId(int userId)
        {
            ResultModel result = new ResultModel();
            try
            {
                //根据ID查询
                result.data = _user.Find<User>(userId);
            }
            catch (Exception ex)
            {
                //出现异常
                result.code = (int)ResultCode.EEEOR;
                result.msg = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据用户名获取用户
        /// </summary>
        /// <returns></returns>
        //[HttpGet]

        [HttpGet]
        public ResultModel GetUserForUsername(string Username)
        {
            ResultModel result = new ResultModel();
            try
            {
                //根据用户名查询
                result.data = _user.Query<User>(P1 => P1.Username.Contains(Username));
            }
            catch (Exception ex)
            {
                //出现异常
                result.code = (int)ResultCode.EEEOR;
                result.msg = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		public ResultModel UpdateUser(User updateUser)
		{
            ResultModel result = new ResultModel();
            try
            {
                //判断数据是否为空
                if (UserHelps.CheckUser(updateUser))
                {
                    result.code = (int)ResultCode.EEEOR;
                    result.msg = "用户名或密码为空";
                    return result;
                }
                //判断用户是否存在
                //1.根据用户id查询数据库
                User user = _user.Find<User>(updateUser.UserID);
                //2.判断数据库是否存在该用户
                if (user!=null)
                {
                    //3.修改用户信息---这里只修改用户名和密码
                    user.Username=updateUser.Username;
                    user.Password=updateUser.Password;
                    _user.Update<User>(user);
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
            return result;
        }

		/// <summary>
		/// 添加用户
		/// </summary>
		/// <returns></returns>
		[HttpPut]
		public ResultModel AddUser(User newUser)
		{
            ResultModel result = new ResultModel();
            try
            {
                //1.判断用户名和密码是否为空-----判断密码长度等也是在这里判断
                if (UserHelps.CheckUser(newUser))
                {
                    result.code = (int)ResultCode.EEEOR;
                    result.msg = "用户名或密码为空";
                    return result;
                }
                //2.根据用户名查询数据库
                User user = _user.Query<User>(P1 => P1.Username.Equals(newUser.Username)).FirstOrDefault();
                //3.判断数据库是否存在相同用户名的用户
                if (user != null)
                {
                    //已经存在相同用户名的用户
                    result.code = (int)ResultCode.EEEOR;
                    result.msg = "已经存在相同用户名的用户";
                    return result;
                }
                //4.保存到数据库
                _user.Insert<User>(newUser);
            }
            catch (Exception ex)
            {
                //出现异常
                result.code = (int)ResultCode.EEEOR;
                result.msg = ex.Message;
            }
            return result;
        }

       

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete]
		public ResultModel DeleteUser(int userId)
		{
			
            ResultModel result = new ResultModel();
            try
            {
                //判断用户是否存在
                User user = _user.Find<User>(userId);
                if (user == null)
                {
                    result.code = (int)ResultCode.EEEOR;
                    result.msg = "用户已经删除";
                    return result;
                }
                //根据用户ID删除
                _user.Delete<User>(userId);
            }
            catch (Exception ex)
            {
                //出现异常
                result.code = (int)ResultCode.EEEOR;
                result.msg = ex.Message;
            }
            return result;
        }
	}
}
