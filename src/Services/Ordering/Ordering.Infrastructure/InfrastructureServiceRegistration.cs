using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Infrastructure.Email;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure
{
    /**We could have just added these dependencies injections, inclucing the once in Application layer, directly into Program.cs 
     * configure method in Odering.API but we grouped them by their layers according to Clean Architecture hence we separated these
     * services registrations dependencies injection for each layer.
     * AddTransient - we used this to inject d EmailService bcos we do not need to create this for every request, once d application 
     * starts up and it's created once that is enough.
     * AddScoped - means d dependency injection will be used as par the request life cycle**/
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));

            //we use typeof here to convert IAsyncRepository to RepositoryBase because of Mediator
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
