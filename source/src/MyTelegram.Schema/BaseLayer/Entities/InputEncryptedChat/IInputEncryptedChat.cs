// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object sets an encrypted chat ID.
/// See <a href="https://corefork.telegram.org/constructor/InputEncryptedChat" />
///</summary>
[JsonDerivedType(typeof(TInputEncryptedChat), nameof(TInputEncryptedChat))]
public interface IInputEncryptedChat : IObject
{
    ///<summary>
    /// Chat ID
    ///</summary>
    int ChatId { get; set; }

    ///<summary>
    /// Checking sum from constructor <a href="https://corefork.telegram.org/constructor/encryptedChat">encryptedChat</a>, <a href="https://corefork.telegram.org/constructor/encryptedChatWaiting">encryptedChatWaiting</a> or <a href="https://corefork.telegram.org/constructor/encryptedChatRequested">encryptedChatRequested</a>
    ///</summary>
    long AccessHash { get; set; }
}
