using System.Reflection;

namespace ConcurrentJobProcessor.Endpoints.Extensions
{
    public static class EndpointExtensions
    {
        public static IServiceCollection AddEndpoints(
            this IServiceCollection services,
            Assembly assembly)
        {
            var endpointTypes = assembly
                .GetExportedTypes()
                .Where(t =>
                    t.IsSubclassOf(typeof(EndpointGroupBase)) &&
                    !t.IsAbstract);

            foreach (var type in endpointTypes)
            {
                services.AddTransient(typeof(EndpointGroupBase), type);
            }

            return services;
        }

        public static WebApplication MapEndpoints(
            this WebApplication app)
        {
            var endpoints = app.Services
                .GetServices<EndpointGroupBase>();

            foreach (var endpoint in endpoints)
            {
                var groupName = endpoint.GroupName ?? endpoint
                    .GetType()
                    .Name
                    .Replace("Endpoints", "")
                    .ToLower();

                var group = app.MapGroup($"/api/{groupName}");

                endpoint.Map(group);
            }

            return app;
        }
    }
}
