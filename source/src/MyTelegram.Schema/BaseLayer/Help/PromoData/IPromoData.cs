// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Info about pinned MTProxy or Public Service Announcement peers.
/// See <a href="https://corefork.telegram.org/constructor/help.PromoData" />
///</summary>
public interface IPromoData : IObject
{
    ///<summary>
    /// Expiry of PSA/MTProxy info
    ///</summary>
    int Expires { get; set; }
}
