// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Confirmation of message receipt
/// See <a href="https://corefork.telegram.org/constructor/ReceivedNotifyMessage" />
///</summary>
[JsonDerivedType(typeof(TReceivedNotifyMessage), nameof(TReceivedNotifyMessage))]
public interface IReceivedNotifyMessage : IObject
{
    ///<summary>
    /// Message ID, for which PUSH-notifications were canceled
    ///</summary>
    int Id { get; set; }

    ///<summary>
    /// Reserved for future use
    ///</summary>
    int Flags { get; set; }
}
