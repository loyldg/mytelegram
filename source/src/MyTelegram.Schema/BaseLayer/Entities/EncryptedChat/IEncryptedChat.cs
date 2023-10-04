// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object contains info on an encrypted chat.
/// See <a href="https://corefork.telegram.org/constructor/EncryptedChat" />
///</summary>
public interface IEncryptedChat : IObject
{
    ///<summary>
    /// Chat ID
    ///</summary>
    int Id { get; set; }
}
