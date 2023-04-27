// ReSharper disable All

namespace MyTelegram.Schema.Channels;

public interface IAdminLogResults : IObject
{
    TVector<MyTelegram.Schema.IChannelAdminLogEvent> Events { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
