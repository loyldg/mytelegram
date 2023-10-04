// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object defines a group.
/// See <a href="https://corefork.telegram.org/constructor/Chat" />
///</summary>
public interface IChat : IObject
{
    ///<summary>
    /// Channel ID
    ///</summary>
    long Id { get; set; }
}
