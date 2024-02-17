using System.Diagnostics.CodeAnalysis;

namespace MyTelegram.Schema;

public static class SerializerObjectMappings
{
    private const uint VectorConstructorId = 0x1cb5c415;
    private static readonly ConcurrentDictionary<Type, Func<IObject>> GenericTypeOfTConstructors = new();
    private static readonly ConcurrentDictionary<uint, Type> TypeMappingDict = new();
    private static readonly ConcurrentDictionary<uint, Func<IObject>> TypeToConstructors = new();

    static SerializerObjectMappings()
    {
        InitTypeMappings();
    }

    public static void CreateConstructIdToTypeMappingsFromAssembly(Assembly tlObjectInThisAssembly)
    {
        var types = tlObjectInThisAssembly.GetTypes();
        foreach (var type in types)
        {
            var attr = type.GetCustomAttribute<TlObjectAttribute>();
            if (attr != null)
            {
                TypeMappingDict.TryAdd(attr.ConstructorId, type);

                // TVector need process using other ways
                if (attr.ConstructorId != VectorConstructorId)
                {
                    TypeToConstructors.TryAdd(attr.ConstructorId,
                        MyReflectionHelper.CompileConstructor<IObject>(type));

                    //TypeToConstructors2.TryAdd(attr.ConstructorId,
                    //    MyReflectionHelper.CompileConstructor<IObject2>(type));
                }
            }
        }
    }

    private static void InitTypeMappings()
    {
        CreateConstructIdToTypeMappingsFromAssembly(typeof(IObject).Assembly);
    }

    public static void TryAddTlObjectFuncToCache(Type typeOfT,
        Func<IObject> func)
    {
        GenericTypeOfTConstructors.TryAdd(typeOfT, func);
    }

    public static bool TryGetTlObject(Type typeOfT,
        [NotNullWhen(true)] out Func<IObject>? func)
    {
        return GenericTypeOfTConstructors.TryGetValue(typeOfT, out func);
    }

    public static bool TryGetTlObject(uint constructorId,
        [NotNullWhen(true)] out Func<IObject>? func)
    {
        return TypeToConstructors.TryGetValue(constructorId, out func);
    }

    public static bool TryGetTlObjectType(uint constructorId, [NotNullWhen(true)] out Type? type)
    {
        return TypeMappingDict.TryGetValue(constructorId, out type);
    }
}
