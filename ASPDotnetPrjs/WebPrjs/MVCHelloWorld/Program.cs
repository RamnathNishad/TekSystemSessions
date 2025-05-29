using EFCodeCodeFirstLib;
using EFCoreDatabaseFirstLib;
using Microsoft.EntityFrameworkCore;

namespace MVCHelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //configure DbContext options
            builder.Services.AddDbContext<EmployeeDbContext>(options =>
            {
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmpDB;Integrated Security=True;");
            });
            //configure DAL component for dependency injection
            builder.Services.AddTransient<IEmpDataAccess,EmpDataAccessLayer>();

            //configure the DbContext for Customer database
            builder.Services.AddDbContext<CustomerDbContext>(options =>
            {
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CustomerDB;Integrated Security=True;");
            });
            //configure DAL component for dependency injection of Customer Data Access class
            builder.Services.AddTransient<ICustomerDataAccess, CustomerDataAcess>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
