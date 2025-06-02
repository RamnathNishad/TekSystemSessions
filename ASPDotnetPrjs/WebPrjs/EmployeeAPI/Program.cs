
using EFCoreDatabaseFirstLib;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers(options =>
            {
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            });
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //configure DbContext options
            builder.Services.AddDbContext<EmployeeDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("constr"));
            });
            //configure DAL component for dependency injection
            builder.Services.AddTransient<IEmpDataAccess, EmpDataAccessLayer>();

            builder.Services.AddScoped<GlobalExceptionHandler, GlobalExceptionHandler>();

            //add CORS policy for client access
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("clients-allowed", opt =>
                {
                    opt.WithOrigins("http://localhost:5132")
                       .AllowAnyMethod();
                });
            });

            //configure jwt authentcation
            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                var key = builder.Configuration["JWT:Key"];
                var byteKey=System.Text.Encoding.UTF8.GetBytes(key); ;

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience= builder.Configuration["JWT:Audience"],
                    ValidIssuer= builder.Configuration["JWT:Issuer"],
                    IssuerSigningKey=new SymmetricSecurityKey(byteKey)
                };
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapControllers();

            app.UseMiddleware<GlobalExceptionHandler>();

            //use CORS policy
            app.UseCors("clients-allowed");

            //it shud in this same sequence
            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
