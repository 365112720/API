
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

			
			//��ʼ�����ݿ�
			using (ShopDbContext context = new ShopDbContext())
			{
				//context.Database.EnsureDeleted();  //�о�ɾ��
				context.Database.EnsureCreated();  //û�д���


			}

			#region IOCע�����

			{
				//	builder.Services.AddTransient<IUserService , UserService>();  //ע���û�����

				builder.Services.AddScoped<IUserService, UserService>();  //д���ע�� ��̨ �û�����

				builder.Services.AddScoped<ICustomerService, CustomerService>(); //ǰ̨ �û�����

				builder.Services.AddScoped<IOrderService, OrderService>(); //�����û�����

				builder.Services.AddScoped<IProductPropertyService, ProductPropertyService>();//��Ʒ��Ϣ����

				builder.Services.AddScoped<DbContext, ShopDbContext>(); //ע��  ���ݷ���

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
						Title = $"�̵����api�ĵ�",
						Version = Version,
						Description = $"ͨ�ð汾coreApi�汾{Version}"
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
						c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"�̵꡾{version}��ҳ");
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