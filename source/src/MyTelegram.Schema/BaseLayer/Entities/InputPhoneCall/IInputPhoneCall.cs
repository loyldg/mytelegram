// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Phone call
/// See <a href="https://corefork.telegram.org/constructor/InputPhoneCall" />
///</summary>
public interface IInputPhoneCall : IObject
{
    ///<summary>
    /// Call ID
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// Access hash
    ///</summary>
    long AccessHash { get; set; }
}
