// ReSharper disable All

namespace MyTelegram.Schema.LayerN;

public interface IChannelParticipant : IObject
{
    Schema.IChannelParticipant Participant { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
