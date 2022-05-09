namespace MyTelegram.Domain.Aggregates.AppCode;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<AppCodeId>))]
public class AppCodeId : MyIdentity<AppCodeId>
{
    public AppCodeId(string value) : base(value)
    {
    }

    public static AppCodeId Create(string phoneNumber,
        string phoneCodeHash)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"{phoneNumber}_{phoneCodeHash}");
    }
}
