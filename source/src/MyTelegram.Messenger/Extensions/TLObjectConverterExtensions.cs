namespace MyTelegram.Messenger.Extensions;

// ReSharper disable once InconsistentNaming
public static class TLObjectConverterExtensions
{
    public static IServiceCollection AddLayeredServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(ILayeredService<>), typeof(LayeredService<>));

        var baseType = typeof(ILayeredConverter);
        var types = typeof(TLObjectConverterExtensions)
            .Assembly
            .GetTypes()
            .Where(p => baseType.IsAssignableFrom(p) && !p.IsAbstract)
            .ToList();

        foreach (var type in types)
        {
            var baseInterfaces = type.GetInterfaces()
                .Where(p => baseType.IsAssignableFrom(p) && p != baseType&&!p.Name.Contains("Layer")).ToList();

            foreach (var baseInterface in baseInterfaces)
            {
                services.AddSingleton(baseInterface, type);
            }
        }

        return services;
    }
}