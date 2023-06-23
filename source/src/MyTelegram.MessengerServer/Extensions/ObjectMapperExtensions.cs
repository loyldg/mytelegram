namespace MyTelegram.MessengerServer.Extensions;

public static class ObjectMapperExtensions
{
    private static void AddImplementedGenericTypes(List<Type> result,
        Type givenType,
        Type genericType)
    {
        var givenTypeInfo = givenType.GetTypeInfo();

        if (givenTypeInfo.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            if (!result.Contains(givenType))
                result.Add(givenType);

        foreach (var interfaceType in givenTypeInfo.GetInterfaces())
            if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                if (!result.Contains(interfaceType))
                    result.Add(interfaceType);

        if (givenTypeInfo.BaseType == null) return;

        AddImplementedGenericTypes(result, givenTypeInfo.BaseType, genericType);
    }

    private static List<Type> GetImplementedGenericTypes(Type givenType,
        Type genericType)
    {
        var result = new List<Type>();
        AddImplementedGenericTypes(result, givenType, genericType);
        return result;
    }

    public static void RegisterAllMappers(this IServiceCollection services)
    {
        //services.AddSingleton<IObjectMapper, DefaultObjectMapper>();
        var mapperType = typeof(CustomObjectMapper);
        var types = GetImplementedGenericTypes(mapperType, typeof(IObjectMapper<,>));
        foreach (var type in types) services.AddSingleton(type, mapperType);
    }
}