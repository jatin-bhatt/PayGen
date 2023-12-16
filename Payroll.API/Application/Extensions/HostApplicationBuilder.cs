using Microsoft.Extensions.Hosting;

namespace Payroll.API.Application.Extensions {
    public static class HostApplicationBuilder {

        public static void AddApplicationServices(this IHostApplicationBuilder builder) {
            // Add the authentication services to DI
            //builder.AddDefaultAuthentication();

            // Configure mediatR
            var services = builder.Services;

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(Program));

                //cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                //cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
                //cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            

        }
    }
}
