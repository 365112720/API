using Microsoft.EntityFrameworkCore;
using Models;

namespace Dtodata
{
    /// <summary>
    /// 当前的类库  是 数据访问层  定义 访问  数据层 DATA 
    /// </summary>
    public class ShopDbContext : DbContext
    {


        public ShopDbContext()
        {

        }

        //这里的话  相当于  使用  这个两个类的类库  继承 db context   第一步写法
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }  //使用  user 类库

        public virtual DbSet<Customer> Customers { get; set; }  //使用  user 类库 

        public virtual DbSet<Address> Addresses { get; set; }  //使用  user 类库 

        public virtual DbSet<Order> Orders { get; set; }  //使用  user 类库 

        public virtual DbSet<OrderDeatils> OrderDeatils { get; set; }  //使用  user 类库 

        public virtual DbSet<Product> Products { get; set; }  //使用  user 类库 

        public virtual DbSet<ProductProperty> ProductProperties { get; set; }  //使用  user 类库 

        public virtual DbSet<ProductType> ProductTypes { get; set; }  //使用  user 类库 

        public virtual DbSet<ShopCar> ShopCars { get; set; }  //使用  user 类库 




        //配置数据库链接  第二步写法
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source=DESKTOP-GTVVK1A;database=Shop2; Integrated Security=True; TrustServerCertificate=true"); //起的什么名字是什么数据库名 必须 database、adminxp  Data Source=.;Integrated Security=True  Data Source=DESKTOP-GTVVK1A;Integrated Security=True
                //  optionsBuilder.UseSqlServer("Data Source=USER-20220605WP\\SQLEXPRESS;database=Shop2; Integrated Security=True; TrustServerCertificate=true"); //起的什么名字是什么数据库名 必须 database、adminxp
                //optionsBuilder.UseSqlServer("server=127.0.0.1;database=Shop2;user=sa;password=123; TrustServerCertificate=true"); //起的什么名字是什么数据库名 必须 database、adminxp
                optionsBuilder.UseSqlServer("server=192.168.40.128;database=Shop2;user=sa;password=SA@12345; TrustServerCertificate=true"); //起的什么名字是什么数据库名 必须 database、adminxp
               //Data Source=USER-20220605WP\SQLEXPRESS;Integrated Security=True  //设置这个证书才能访问数据库TrustServerCertificate=true
            }

            base.OnConfiguring(optionsBuilder);
        }


        //定义反射表的关系  第三步 写法
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);  这里 定义两张表 分别 user 和 订单  order
            modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
        });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");
            });
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");
            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");
            });
            modelBuilder.Entity<OrderDeatils>(entity =>
            {
                entity.ToTable("OrderDeatils");
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
            });
            modelBuilder.Entity<ProductProperty>(entity =>
            {
                entity.ToTable("ProductProperty");
            });
            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductType");
            });
            modelBuilder.Entity<ShopCar>(entity =>
            {
                entity.ToTable("ShopCar");
            });

        }
    }
}