// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Contains info on message sent to an encrypted chat.
/// See <a href="https://corefork.telegram.org/constructor/messages.SentEncryptedMessage" />
///</summary>
public interface ISentEncryptedMessage : IObject
{
    ///<summary>
    /// Sending date
    ///</summary>
    int Date { get; set; }
}
