// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Update of Telegram's terms of service
/// See <a href="https://corefork.telegram.org/constructor/help.TermsOfServiceUpdate" />
///</summary>
public interface ITermsOfServiceUpdate : IObject
{
    ///<summary>
    /// New TOS updates will have to be queried using <a href="https://corefork.telegram.org/method/help.getTermsOfServiceUpdate">help.getTermsOfServiceUpdate</a> in <code>expires</code> seconds
    ///</summary>
    int Expires { get; set; }
}
