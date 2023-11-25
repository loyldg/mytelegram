// ReSharper disable All

namespace MyTelegram.Schema.Channels;

///<summary>
/// Admin log events
/// See <a href="https://corefork.telegram.org/constructor/channels.AdminLogResults" />
///</summary>
[JsonDerivedType(typeof(TAdminLogResults), nameof(TAdminLogResults))]
public interface IAdminLogResults : IObject
{
    ///<summary>
    /// Admin log events
    /// See <a href="https://corefork.telegram.org/type/ChannelAdminLogEvent" />
    ///</summary>
    TVector<MyTelegram.Schema.IChannelAdminLogEvent> Events { get; set; }

    ///<summary>
    /// Chats mentioned in events
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Users mentioned in events
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
