using CorgiVR.Repository;
using CorgiVR.Repository.Contract;
using CorgiVR.Services;
using CorgiVR.Services.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CorgiVR.Common
{
    public static class ServicesCoufiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(ILoyalityRepository), typeof(LoyalityRepository));
            services.AddTransient(typeof(ILoyalityService), typeof(LoyalityService));
            var dbPath = configuration["DbPath"];
            services.AddDbContextPool<EfContext>(
                                                 options => options.UseSqlite($"Data Source={dbPath}"));
        }
    }
}