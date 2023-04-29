using System.Text.Json.Serialization;

namespace MyTelegram.MessengerServer.Services.Serialization.SystemTextJson;

[JsonSerializable(typeof(Metadata))]
[JsonSerializable(typeof(SnapshotMetadata))]
[JsonSerializable(typeof(PublishCommandJob))]
[JsonSerializable(typeof(DispatchToAsynchronousEventSubscribersJob))]
public partial class MyJsonContext : JsonSerializerContext
{
}
