// ReSharper disable All

namespace MyTelegram.Schema.Channels;

public interface IChannelParticipant : IObject
{
    Schema.IChannelParticipant Participant { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
