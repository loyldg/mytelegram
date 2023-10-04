// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A group call
/// See <a href="https://corefork.telegram.org/constructor/GroupCall" />
///</summary>
public interface IGroupCall : IObject
{
    ///<summary>
    /// Group call ID
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// Group call access hash
    ///</summary>
    long AccessHash { get; set; }
}
