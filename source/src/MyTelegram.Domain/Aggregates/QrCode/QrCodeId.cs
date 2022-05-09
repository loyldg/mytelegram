namespace MyTelegram.Domain.Aggregates.QrCode;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<QrCodeId>))]
public class QrCodeId : MyIdentity<QrCodeId>
{
    public QrCodeId(string value) : base(value)
    {
    }

    public static QrCodeId Create(string token)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands, $"qrcode-{token}");
    }
}
