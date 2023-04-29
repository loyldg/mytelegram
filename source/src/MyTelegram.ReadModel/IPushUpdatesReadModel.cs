namespace MyTelegram.ReadModel;

public interface IPushUpdatesReadModel : IReadModel
{
    byte[] Data { get; }
    long ExcludeAuthKeyId { get; set; }
    string Id { get; }
    long OnlySendToThisAuthKeyId { get; }
    long PeerId { get; }
    int Pts { get; }
    PtsType PtsType { get; }
    long SeqNo { get; }
}
