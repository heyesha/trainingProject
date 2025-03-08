using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrainingProject.DAL.Interceptors;
using TrainingProject.DAL.Repositories;
using TrainingProject.Domain.Entity;
using TrainingProject.Domain.Interfaces.Repositories;

namespace TrainingProject.DAL.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MSSQL");

            services.AddDbContext<ApplicationDbContext>(options => 
            { 
                options.UseSqlServer(connectionString); 
            });
            services.AddSingleton<DateInterceptor>();
            services.InitRepositories();
        }

        private static void InitRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
            services.AddScoped<IBaseRepository<Report>, BaseRepository<Report>>();
        }
    }
}
