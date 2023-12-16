using NodaTime;

namespace Application.Extensions {
    public static class ServiceCollection {
        public static void AddApplicationServices(this IServiceCollection services) {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            services.AddSingleton<IClock>(SystemClock.Instance);
        }
    }
}
