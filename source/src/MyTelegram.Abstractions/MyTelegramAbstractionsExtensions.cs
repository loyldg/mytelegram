using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MyTelegram.Abstractions;

public static class MyTelegramAbstractionsExtensions
{
    public static void RegisterServices(this IServiceCollection services, Assembly? assembly = null)
    {
        assembly ??= Assembly.GetCallingAssembly();

        var singletonMarker = typeof(ISingletonDependency);
        var transientMarker = typeof(ITransientDependency);

        foreach (var type in assembly.GetTypes())
        {
            if (!type.IsClass || type.IsAbstract)
            {
                continue;
            }

            var lifetime = type.GetInterfaces() switch
            {
                var interfaces when interfaces.Contains(singletonMarker)
                    => ServiceLifetime.Singleton,

                var interfaces when interfaces.Contains(transientMarker)
                    => ServiceLifetime.Transient,

                _ => (ServiceLifetime?)null
            };

            if (lifetime is null)
            {
                continue;
            }

            foreach (var serviceType in type.GetInterfaces())
            {
                if (serviceType == singletonMarker ||
                    serviceType == transientMarker)
                {
                    continue;
                }

                services.Add(new ServiceDescriptor(
                    serviceType,
                    type,
                    lifetime.Value));
            }

            services.Add(new ServiceDescriptor(
                type,
                type,
                lifetime.Value));
        }
    }
}