namespace MyTelegram.ReadModel;

public interface IUpdatesReadModel : IReadModel
{
    long OwnerPeerId { get; }
    long ChannelId { get; }

    ///// <summary>
    ///// Only for channel updates
    ///// </summary>
    //long? ChannelId { get; }
    long? ExcludeAuthKeyId { get; }
    long? ExcludeUserId { get; }
    long? OnlySendToUserId { get; }
    long? OnlySendToThisAuthKeyId { get; }
    //PtsType PtsType { get; }
    UpdatesType UpdatesType { get; set; }
    int? MessageId { get; }
    int Pts { get; }
    int Date { get; }
    long GlobalSeqNo { get; }
    byte[] Updates { get; }
    List<long>? Users { get; }
    List<long>? Chats { get; }
}