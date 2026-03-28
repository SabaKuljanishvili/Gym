using GymMembershipManagement.API.Middleware;
using GymMembershipManagement.DAL.Repositories;
using GymMembershipManagement.DATA;
using GymMembershipManagement.SERVICE;
using GymMembershipManagement.SERVICE.Interfaces;
using GymMembershipManagement.SERVICE.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GymMembershipManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. CORS კონფიგურაცია
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()   // ნებას რთავს ნებისმიერ საიტს
                          .AllowAnyMethod()   // ნებას რთავს POST, PUT, DELETE, GET
                          .AllowAnyHeader();  // ნებას რთავს ნებისმიერ Header-ს (Content-Type და ა.შ.)
                });
            });

            // DB connection
            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("GymDbConnection"));
            });

            builder.Services.AddScoped<DbContext, GymDbContext>();

            // AutoMapper
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(), typeof(MappingProfile).Assembly);

            // Controllers
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Gym Membership API",
                    Version = "v1"
                });
            });

            // Repositories
            builder.Services.AddScoped<IGymClassRepository, GymClassRepository>();
            builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPersonRepository, PersonRepository>();
            builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();

            // Services
            builder.Services.AddScoped<IGymClassService, GymClassService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IMembershipService, MembershipService>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();

            var app = builder.Build();

            // 2. CORS Middleware-ის გააქტიურება (მნიშვნელოვანია იყოს Routing-სა და Authorization-ს შორის)
            app.UseCors("AllowAll");

            // ავტომატური მიგრაციის ბლოკი
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Migration error: {ex.Message}");
                }
            }

            // Global exception handling
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gym API V1");
                c.RoutePrefix = "swagger";
            });

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger/index.html");
                return Task.CompletedTask;
            });

            app.Run();
        }
    }
}