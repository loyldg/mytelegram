// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Phone call
/// See <a href="https://corefork.telegram.org/constructor/PhoneCall" />
///</summary>
public interface IPhoneCall : IObject
{
    ///<summary>
    /// Call ID
    ///</summary>
    long Id { get; set; }
}
