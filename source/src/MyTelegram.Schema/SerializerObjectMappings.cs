namespace MyTelegram.Schema;

public static class SerializerObjectMappings
{
    private const uint VectorConstructorId = 0x1cb5c415;
    private static readonly ConcurrentDictionary<Type, Func<IObject>> GenericTypeOfTConstructors = new();
    private static readonly ConcurrentDictionary<uint, Type> TypeMappingDict = new();
    private static readonly ConcurrentDictionary<uint, Func<IObject>> TypeToConstructors = new();

    //private static readonly ConcurrentDictionary<Type, Func<IObject2>> GenericTypeOfTConstructors2 = new();
    //private static readonly ConcurrentDictionary<uint, Func<IObject2>> TypeToConstructors2 = new();


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

    //public static void TryAddTlObjectFuncToCache2(Type typeOfT,
    //    Func<IObject2> func)
    //{
    //    GenericTypeOfTConstructors2.TryAdd(typeOfT, func);
    //}

    public static bool TryGetTlObject(Type typeOfT,
        out Func<IObject>? func)
    {
        return GenericTypeOfTConstructors.TryGetValue(typeOfT, out func);
    }

    //public static bool TryGetTlObject2(Type typeOfT,
    //    out Func<IObject2>? func)
    //{
    //    return GenericTypeOfTConstructors2.TryGetValue(typeOfT, out func);
    //}

    public static bool TryGetTlObject(uint constructorId,
        out Func<IObject>? func)
    {
        return TypeToConstructors.TryGetValue(constructorId, out func);
    }

    //public static bool TryGetTlObject2(uint constructorId,
    //    out Func<IObject2>? func)
    //{
    //    return TypeToConstructors2.TryGetValue(constructorId, out func);
    //}
}
