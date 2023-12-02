using System.Text.Json.Serialization;

namespace MyTelegram.GatewayServer.NativeAot;

//[JsonSerializable(typeof(MTProto.EncryptedMessage))]
//[JsonSerializable(typeof(MTProto.EncryptedMessageResponse))]
//[JsonSerializable(typeof(MTProto.UnencryptedMessage))]
//[JsonSerializable(typeof(MTProto.UnencryptedMessageResponse))]

[JsonSerializable(typeof(MyTelegram.Core.EncryptedMessage))]
[JsonSerializable(typeof(MyTelegram.Core.EncryptedMessageResponse))]
[JsonSerializable(typeof(MyTelegram.Core.UnencryptedMessage))]
[JsonSerializable(typeof(MyTelegram.Core.UnencryptedMessageResponse))]

[JsonSerializable(typeof(AuthKeyNotFoundEvent))]
[JsonSerializable(typeof(MyTelegram.Core.ClientDisconnectedEvent))]

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]

public partial class GatewayServerJsonContext : JsonSerializerContext
{
}