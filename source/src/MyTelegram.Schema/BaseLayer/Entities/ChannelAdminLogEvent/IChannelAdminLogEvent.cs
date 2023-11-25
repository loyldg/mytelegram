// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// An event in a channel admin log
/// See <a href="https://corefork.telegram.org/constructor/ChannelAdminLogEvent" />
///</summary>
[JsonDerivedType(typeof(TChannelAdminLogEvent), nameof(TChannelAdminLogEvent))]
public interface IChannelAdminLogEvent : IObject
{
    ///<summary>
    /// Event ID
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// Date
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// User ID
    ///</summary>
    long UserId { get; set; }

    ///<summary>
    /// Action
    /// See <a href="https://corefork.telegram.org/type/ChannelAdminLogEventAction" />
    ///</summary>
    MyTelegram.Schema.IChannelAdminLogEventAction Action { get; set; }
}
