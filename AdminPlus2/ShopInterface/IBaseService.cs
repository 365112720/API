
using System.Linq.Expressions;

using System.Data.SqlClient;

namespace ShopInterface
{
 public	interface BaseService
	{
		/// <summary>
		/// 根据id实体来查询
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="id"></param>
		/// <returns></returns>
		#region 查询
		T Find<T>(int id)where T :class ;

		[Obsolete("尽量避免表达树，using带表达式目录树 替代")]

		IQueryable<T> Set<T>() where T : class ;

		/// <summary>
		/// 查询 集合
		/// </summary>
	    /// <returns>IQueryable类型集合</returns>
		IQueryable<T> Query<T>(Expression<Func<T,bool>> funcWhere) where T : class ;

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

		PagingData<T> QuseryPage<T,S>(Expression<Func<T,IUserService>> funcWhere,int pageSize,int PageIndex, Expression<Func<T,S>>funcOrderby,bool isAsc = true) where T : class ;
		#endregion

		
		#region 添加
		/// <summary>
		/// 新增数据，即时Commit
		/// </summary>
		/// <param name="t"></param>
		/// <returns>返回带主键的实体</returns>
		T Insert<T>(T t) where T : class;

		/// <summary>
		/// 新增数据，即时Commit
		/// 多条sql 一个连接，事务插入
		/// </summary>
		/// <param name="tList"></param>
		IEnumerable<T> Insert<T>(IEnumerable<T> tList) where T : class;
		#endregion


		#region 更新
		/// <summary>
		/// 更新数据，即时Commit
		/// </summary>
		/// <param name="t"></param>
		void Update<T>(T t) where T : class;

		/// <summary>
		/// 更新数据，即时Commit
		/// </summary>
		/// <param name="tList"></param>
		void Update<T>(IEnumerable<T> tList) where T : class;
		#endregion


		#region 删除
		/// <summary>
		/// 根据主键删除数据，即时Commit
		/// </summary>
		/// <param name="t"></param>
		void Delete<T>(int Id) where T : class;

		/// <su+mary>
		/// 删除数据，即时Commit
		/// </summary>
		/// <param name="t"></param>
		void Delete<T>(T t) where T : class;

		/// <summary>
		/// 删除数据，即时Commit
		/// </summary>
		/// <param name="tList"></param>
		void Delete<T>(IEnumerable<T> tList) where T : class;
		#endregion


		#region Other其他 无返值
		/// <summary>
		/// 立即保存全部修改
		/// 把增/删的savechange给放到这里，是为了保证事务的
		/// </summary>
		void Commit();

		/// <summary>
		/// 执行sql 返回集合
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		IQueryable<T> ExcuteQuery<T>(string sql, SqlParameter[] parameters) where T : class;

		/// <summary>
		/// 执行sql，无返回
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="parameters"></param>
		void Excute<T>(string sql, SqlParameter[] parameters) where T : class;

		#endregion 


		#region 测试伪代码
		public void Add();


		public void Update();

		public void Delete();

		public void DeleteAll();
		#endregion

	}
}
