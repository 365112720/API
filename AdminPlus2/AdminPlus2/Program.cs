
using AdminXP.Utility.SwaggerExt;
using Dtodata;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShopInterface;
using ShopService;

namespace AdminPlus2
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			
			//初始化数据库
			using (ShopDbContext context = new ShopDbContext())
			{
				//context.Database.EnsureDeleted();  //有就删除
				context.Database.EnsureCreated();  //没有创建


			}

			#region IOC注册服务

			{
				//	builder.Services.AddTransient<IUserService , UserService>();  //注册用户服务

				builder.Services.AddScoped<IUserService, UserService>();  //写这个注册 后台 用户服务

				builder.Services.AddScoped<ICustomerService, CustomerService>(); //前台 用户服务

				builder.Services.AddScoped<IOrderService, OrderService>(); //订单用户服务

				builder.Services.AddScoped<IProductPropertyService, ProductPropertyService>();//商品信息服务

				builder.Services.AddScoped<DbContext, ShopDbContext>(); //注册  数据服务

			}
			#endregion

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(Option =>
			{
				typeof(ApiVersions).GetEnumNames().ToList().ForEach(Version =>
				{
					Option.SwaggerDoc(Version, new OpenApiInfo()
					{
						Title = $"商店管理api文档",
						Version = Version,
						Description = $"通用版本coreApi版本{Version}"
					});
				});

				var file = Path.Combine(AppContext.BaseDirectory, "AdminPlus2.xml");

				Option.IncludeXmlComments(file, true);

				Option.OrderActionsBy(o => o.RelativePath);


			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					foreach (string version in typeof(ApiVersions).GetEnumNames())
					{
						c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"商店【{version}】页");
					}
				});
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}