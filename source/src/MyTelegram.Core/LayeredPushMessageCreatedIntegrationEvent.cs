using System.Buffers;

namespace MyTelegram.Core;

public record LayeredPushMessageCreatedIntegrationEvent(
    PeerType PeerType,
    long PeerId,
    ReadOnlyMemory<byte> Data,
    long? ExcludeAuthKeyId,
    long? ExcludeUserId,
    long? OnlySendToUserId,
    long? OnlySendToThisAuthKeyId,
    int Pts,
    int? Qts,
    long GlobalSeqNo,
    //LayeredData<byte[]>? LayeredData,
    PushData? PushData,
    List<long>? ExcludeUserIds
    ) : ISessionMessage
{
    public IMemoryOwner<byte>? MemoryOwner { get; set; }
}

public record LayeredPushMessageCreatedIntegrationEvent<TExtraData>(
    PeerType PeerType,
    long PeerId,
    ReadOnlyMemory<byte> Data,
    long? ExcludeAuthKeyId,
    long? ExcludeUserId,
    long? OnlySendToUserId,
    long? OnlySendToThisAuthKeyId,
    int Pts,
    int? Qts,
    long GlobalSeqNo,
    //LayeredData<byte[]>? LayeredData,
    TExtraData ExtraData,
    PushData? PushData,
    List<long>? ExcludeUserIds
    ) :
    LayeredPushMessageCreatedIntegrationEvent(PeerType,
        PeerId,
        Data,
        ExcludeAuthKeyId,
        ExcludeUserId,
        OnlySendToUserId,
        OnlySendToThisAuthKeyId,
        Pts,
        Qts,
        GlobalSeqNo,
        //LayeredData,
        PushData, ExcludeUserIds);