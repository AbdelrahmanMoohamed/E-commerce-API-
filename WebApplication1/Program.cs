using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Exstensions;
using Talabat.APIs.MiddleWares;
using Talabat.Core.Entities.Identity;
using Talabat.Reposatory.Data;
using Talabat.Reposatory.Identity;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            #region Configure Services -Add services to the container.
            builder.Services.AddControllers(); //serviceses for APIs
            // Learn more about configuring Swagger/OpenAPI at
            // https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();//swagger
            builder.Services.AddSwaggerGen(); //swagger
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });



            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var Connection = builder.Configuration.GetConnectionString("RadisConnection");

                return ConnectionMultiplexer.Connect(Connection);

            });
            #endregion

            builder.Services.AddApplicationServices();

            builder.Services.IdentityServices();


            var app = builder.Build();

            #region Update Database
            //StoreContext dbContext = new StoreContext();
            //await dbContext.Database.MigrateAsync();

            using var Scope = app.Services.CreateScope();//for Catch All Services That Time [Scoped]

            var Services = Scope.ServiceProvider;//Catch the its Services Them Self

            var FactoryLogger = Services.GetRequiredService<ILoggerFactory>();

            try
            {
                var dbContext = Services.GetRequiredService<StoreContext>();//Ask CLR To Create Object From DbContext Explicitly

                await dbContext.Database.MigrateAsync();//Update Database 

                var IdentutyDbContext = Services.GetRequiredService<AppIdentityDbContext>();

                await IdentutyDbContext.Database.MigrateAsync();

                var UserMAnager = Services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUSerAsunc(UserMAnager);

                await StoreContextSeed.SeedAsync(dbContext);

            }
            catch (Exception ex)
            {
                var Logger = FactoryLogger.CreateLogger<Program>();
                Logger.LogError(ex, "An Error Ocurred During Appling The Migration");
            }
            #endregion


            #region Configure - Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleWare>();
                app.UseSwaggerMiddleWares();
            }

            app.UseStaticFiles();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();

            app.UseAuthorization();

                
            app.MapControllers(); 
            #endregion


            app.Run();
        }
    }
}
