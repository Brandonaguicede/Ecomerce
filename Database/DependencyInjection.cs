using Database.EcommerceDbContext;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class DependencyInjection
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            services.AddScoped<MyEcommerceDbContext>();
        }
    }
}