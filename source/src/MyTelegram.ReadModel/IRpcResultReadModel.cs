namespace MyTelegram.ReadModel;

public interface IRpcResultReadModel : IReadModel
{
    string Id { get; }
    long PeerId { get; }
    long ReqMsgId { get; }
    byte[] RpcData { get; }
    string SourceId { get; }
}