using MyTelegram.Messenger.TLObjectConverters.Mappers;

namespace MyTelegram.Messenger.Extensions;

public static class ObjectMapperExtensions
{
    public static void RegisterAllMappers(this IServiceCollection services)
    {
        var baseType = typeof(ILayeredMapper);
        var mapperTypes = typeof(ObjectMapperExtensions).Assembly.GetTypes()
            .Where(p => baseType.IsAssignableFrom(p) && !p.IsAbstract)
            .ToList();

        foreach (var mapperType in mapperTypes)
        {
            services.RegisterObjectMapper(mapperType);
        }
    }

    private static void RegisterObjectMapper(this IServiceCollection services, Type mapperType)
    {
        var types = GetImplementedGenericTypes(mapperType, typeof(IObjectMapper<,>));
        foreach (var type in types)
        {
            services.AddSingleton(type, mapperType);
        }
    }

    public static List<Type> GetImplementedGenericTypes(Type givenType, Type genericType)
    {
        var result = new List<Type>();
        AddImplementedGenericTypes(result, givenType, genericType);
        return result;
    }

    private static void AddImplementedGenericTypes(List<Type> result, Type givenType, Type genericType)
    {
        var givenTypeInfo = givenType.GetTypeInfo();

        if (givenTypeInfo.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
        {
            //result.AddIfNotContains(givenType);
            if (!result.Contains(givenType))
            {
                result.Add(givenType);
            }
        }

        foreach (var interfaceType in givenTypeInfo.GetInterfaces())
        {
            if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
            {
                //result.AddIfNotContains(interfaceType);
                if (!result.Contains(interfaceType))
                {
                    result.Add(interfaceType);
                }
            }
        }

        if (givenTypeInfo.BaseType == null)
        {
            return;
        }

        AddImplementedGenericTypes(result, givenTypeInfo.BaseType, genericType);
    }
}
