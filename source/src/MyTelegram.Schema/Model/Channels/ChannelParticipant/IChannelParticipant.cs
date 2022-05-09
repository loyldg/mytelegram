// ReSharper disable All

namespace MyTelegram.Schema.Channels;

public interface IChannelParticipant : IObject
{
    MyTelegram.Schema.IChannelParticipant Participant { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
