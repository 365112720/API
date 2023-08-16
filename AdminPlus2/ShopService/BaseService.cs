using ShopInterface;
using Microsoft.EntityFrameworkCore; //引入成功
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ShopService
{
	public class BaseService : ShopInterface.BaseService
	{

		protected DbContext Context { get; set; } //需要引入 程序集 efcoer Microsoft.Entity

		public BaseService (DbContext context) //写了个构造函数
		{
			Context = context;
		}

		//实现具体方法
		#region Query 查询
		public T Find<T>(int id) where T : class
		{
			return Context.Set<T>().Find(id);
		}

		/// <summary>
		///  不应该暴露给上端使用者，尽量少用
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		//[Obsolete("尽量避免使用，using 带表达式目录树的代替")]
		public IQueryable<T> Set<T>() where T : class
		{
			return Context.Set<T>();
		}

		/// <summary>
		/// 这才是合理的做法，上端给条件，这里查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="funcWhere"></param>
		/// <returns></returns>
		public IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class
		{
			return Context.Set<T>().Where(funcWhere);
		}

		/// <summary>
		/// 分页查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="S"></typeparam>
		/// <param name="funcWhere"></param>
		/// <param name="pageSize"></param>
		/// <param name="pageIndex"></param>
		/// <param name="funcOrderby"></param>
		/// <param name="isAsc"></param>
		/// <returns></returns>
		public PagingData<T> QueryPage<T, S>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex, Expression<Func<T, S>> funcOrderby, bool isAsc = true) where T : class
		{

			var list = Set<T>();
			if (funcWhere != null)
			{
				list = list.Where(funcWhere);
			}
			if (isAsc)
			{
				list = list.OrderBy(funcOrderby);
			}
			else
			{
				list = list.OrderByDescending(funcOrderby);
			}
			PagingData<T> result = new PagingData<T>()
			{
				DATAlist = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
				PageIndex = pageIndex,
				pageSize = pageSize,
				Count = list.Count()
			};
			return result;
		}
		#endregion

		#region Insert 添加
		/// <summary>
		/// 即使保存  不需要再Commit
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="t"></param>
		/// <returns></returns>
		public T Insert<T>(T t) where T : class
		{
			Context.Set<T>().Add(t);
			Commit();//写在这里  就不需要单独commit  不写就需要
			return t;
		}

		public IEnumerable<T> Insert<T>(IEnumerable<T> tList) where T : class
		{
			Context.Set<T>().AddRange(tList);
			Commit();//一个链接  多个sql
			return tList;
		}
		#endregion

		#region Update 更新
		/// <summary>
		/// 是没有实现查询，直接更新的,需要Attach和State
		/// 
		/// 如果是已经在context，只能再封装一个(在具体的service)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="t"></param>
		public void Update<T>(T t) where T : class
		{

			if (t == null) throw new Exception("t is null");

			Context.Set<T>().Attach(t);//将数据附加到上下文，支持实体修改和新实体，重置为UnChanged
			Context.Entry(t).State = EntityState.Modified;
			Commit();//保存 然后重置为UnChanged
		}

		public void Update<T>(IEnumerable<T> tList) where T : class
		{

			foreach (var t in tList)
			{
				Context.Set<T>().Attach(t);
				Context.Entry(t).State = EntityState.Modified;
			}
			Commit();
		}

		#endregion

		#region Delete  删除
		/// <summary>
		/// 先附加 再删除
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="t"></param>
		public void Delete<T>(T t) where T : class
		{

			if (t == null) throw new Exception("t is null");
			Context.Set<T>().Attach(t);
			Context.Set<T>().Remove(t);
			Commit();
		}

		/// <summary>
		/// 还可以增加非即时commit版本的，
		/// 做成protected
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="Id"></param>
		public void Delete<T>(int Id) where T : class
		{

			T t = Find<T>(Id);//也可以附加
			if (t == null) throw new Exception("t is null");
			Context.Set<T>().Remove(t);
			Commit();
		}

		public void Delete<T>(IEnumerable<T> tList) where T : class
		{

			foreach (var t in tList)
			{
				Context.Set<T>().Attach(t);
			}
			Context.Set<T>().RemoveRange(tList);
			Commit();
		}
		#endregion

		#region Other 其他
		public void Commit()
		{
			Context.SaveChanges(); //EFCore中对于增删改 ,必须要执行这句话才能生效
		}

		public IQueryable<T> ExcuteQuery<T>(string sql, SqlParameter[] parameters) where T : class
		{
			return null;
		}

		public void Excute<T>(string sql, SqlParameter[] parameters) where T : class
		{
			IDbContextTransaction trans = null;  //引入程序集 事务支持
			//DbContextTransaction trans = null;
			try
			{
				trans = Context.Database.BeginTransaction();
				//this.Context.Database.ExecuteSqlRaw(sql, parameters);
				trans.Commit();
			}
			catch (Exception)
			{
				if (trans != null)
					trans.Rollback();
				throw;
			}
		}

		public virtual void Dispose()
		{
			if (Context != null)
			{
				Context.Dispose();
			}
		}
		#endregion

















		#region 测试
		/// <summary>
		/// 增删改查
		/// </summary>
		/// <exception cref="NotImplementedException"></exception>
		public void Add()
		{
			throw new NotImplementedException();
		}

		public void Delete()
		{
			throw new NotImplementedException();
		}

		public void DeleteAll()
		{
			throw new NotImplementedException();
		}

		public void Update()
		{
			throw new NotImplementedException();
		}

		public PagingData<T> QuseryPage<T, S>(Expression<Func<T, IUserService>> funcWhere, int pageSize, int PageIndex, Expression<Func<T, S>> funcOrderby, bool isAsc = true) where T : class
		{
			throw new NotImplementedException();
		}
		#endregion




	}
}
