namespace Ordering.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        //services.AddCarter();

        return services;
    }

    public static WebApplication AddApiServices(this WebApplication app)
    {
        //services.AddCarter();

        return app;
    }
}