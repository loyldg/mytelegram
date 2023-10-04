// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Info about the support user, relevant to the current user.
/// See <a href="https://corefork.telegram.org/constructor/help.Support" />
///</summary>
public interface ISupport : IObject
{
    ///<summary>
    /// Phone number
    ///</summary>
    string PhoneNumber { get; set; }

    ///<summary>
    /// User
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    MyTelegram.Schema.IUser User { get; set; }
}
