using SpanJson;
using SpanJson.Resolvers;

namespace MyTelegram.MessengerServer.Services.Serialization.SpanJson;

public sealed class CustomResolver<TSymbol> : ResolverBase<TSymbol, CustomResolver<TSymbol>> where TSymbol : struct
{
    public CustomResolver() : base(new SpanJsonOptions()
    {
        EnumOption = EnumOptions.Integer,
        NullOption = NullOptions.ExcludeNulls,
    })
    {
        RegisterGlobalCustomFormatter<byte[], BytesToBase64StringFormatter>();
    }

    protected override void TryGetAnnotatedAttributeConstructor(Type type,
        out ConstructorInfo? constructor,
        out JsonConstructorAttribute? attribute)
    {
        constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
            .FirstOrDefault(a => a.GetCustomAttribute<JsonConstructorAttribute>() != null);
        if (constructor != null)
        {
            attribute = constructor.GetCustomAttribute<JsonConstructorAttribute>();
            return;
        }

        if (TryGetBaseClassJsonConstructorAttribute(type, out attribute) || type.GetMethod("<Clone>$") != null)
        {
            // We basically take the one with the most parameters, this needs to match the dictionary // TODO find better method
            constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).MaxBy(a => a.GetParameters().Length);
            return;
        }

        constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault();

        //constructor = default;
        attribute = default;
    }
}