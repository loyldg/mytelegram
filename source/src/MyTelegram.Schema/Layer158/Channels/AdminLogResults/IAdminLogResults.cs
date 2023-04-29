// ReSharper disable All

namespace MyTelegram.Schema.Channels;

public interface IAdminLogResults : IObject
{
    TVector<Schema.IChannelAdminLogEvent> Events { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
