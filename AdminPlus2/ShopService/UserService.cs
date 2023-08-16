
using Microsoft.EntityFrameworkCore;
using ShopInterface;

namespace ShopService
{
	public class UserService : BaseService, IUserService  //这里有一个报错  user  没有提供 context 参赛
	{
		//public void Add()
		//{
		//	throw new NotImplementedException();
		//}

		//public void Delete()
		//{
		//	throw new NotImplementedException();
		//}

		//public void DeleteAll()
		//{
		//	throw new NotImplementedException();
		//}

		//public void Update()
		//{
		//	throw new NotImplementedException();
		//}
		public UserService(DbContext context) : base(context)  //生成的构造函数 
		{
		}
	}
}
