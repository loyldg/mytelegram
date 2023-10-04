// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Web authorization
/// See <a href="https://corefork.telegram.org/constructor/WebAuthorization" />
///</summary>
public interface IWebAuthorization : IObject
{
    ///<summary>
    /// Authorization hash
    ///</summary>
    long Hash { get; set; }

    ///<summary>
    /// Bot ID
    ///</summary>
    long BotId { get; set; }

    ///<summary>
    /// The domain name of the website on which the user has logged in.
    ///</summary>
    string Domain { get; set; }

    ///<summary>
    /// Browser user-agent
    ///</summary>
    string Browser { get; set; }

    ///<summary>
    /// Platform
    ///</summary>
    string Platform { get; set; }

    ///<summary>
    /// When was the web session created
    ///</summary>
    int DateCreated { get; set; }

    ///<summary>
    /// When was the web session last active
    ///</summary>
    int DateActive { get; set; }

    ///<summary>
    /// IP address
    ///</summary>
    string Ip { get; set; }

    ///<summary>
    /// Region, determined from IP address
    ///</summary>
    string Region { get; set; }
}
