namespace MyTelegram.Domain.Aggregates.Dialog;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<DialogId>))]
public class DialogId : MyIdentity<DialogId>
{
    public DialogId(string value) : base(value)
    {
    }

    public static DialogId Create(long ownerId,
        PeerType toPeerType,
        long toPeerId)
    {
        return NewDeterministic(GuidFactories.Deterministic.Namespaces.Commands,
            $"dialog_{ownerId}_{toPeerType}_{toPeerId}");
    }

    public static DialogId Create(long ownerId,
        Peer toPeer)
    {
        return Create(ownerId, toPeer.PeerType, toPeer.PeerId);
    }
}
