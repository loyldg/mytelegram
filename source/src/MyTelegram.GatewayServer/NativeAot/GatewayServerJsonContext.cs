using System.Text.Json.Serialization;

namespace MyTelegram.GatewayServer.NativeAot;

[JsonSerializable(typeof(MyTelegram.MTProto.EncryptedMessageResponse))]
[JsonSerializable(typeof(MyTelegram.Core.EncryptedMessageResponse))]
[JsonSerializable(typeof(EncryptedMessage))]
[JsonSerializable(typeof(UnencryptedMessage))]
[JsonSerializable(typeof(MyTelegram.MTProto.UnencryptedMessageResponse))]
[JsonSerializable(typeof(MyTelegram.Core.UnencryptedMessageResponse))]
[JsonSerializable(typeof(AuthKeyNotFoundEvent))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]

public partial class GatewayServerJsonContext : JsonSerializerContext
{
}