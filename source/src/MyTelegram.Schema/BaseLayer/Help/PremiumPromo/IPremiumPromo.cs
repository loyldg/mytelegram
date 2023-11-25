// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Telegram Premium promotion information
/// See <a href="https://corefork.telegram.org/constructor/help.PremiumPromo" />
///</summary>
[JsonDerivedType(typeof(TPremiumPromo), nameof(TPremiumPromo))]
public interface IPremiumPromo : IObject
{
    ///<summary>
    /// Description of the current state of the user's Telegram Premium subscription
    ///</summary>
    string StatusText { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/entities">Message entities for styled text</a>
    /// See <a href="https://corefork.telegram.org/type/MessageEntity" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessageEntity> StatusEntities { get; set; }

    ///<summary>
    /// A list of <a href="https://corefork.telegram.org/api/premium">premium feature identifiers »</a>, associated to each video
    ///</summary>
    TVector<string> VideoSections { get; set; }

    ///<summary>
    /// A list of videos
    /// See <a href="https://corefork.telegram.org/type/Document" />
    ///</summary>
    TVector<MyTelegram.Schema.IDocument> Videos { get; set; }

    ///<summary>
    /// Telegram Premium subscription options
    /// See <a href="https://corefork.telegram.org/type/PremiumSubscriptionOption" />
    ///</summary>
    TVector<MyTelegram.Schema.IPremiumSubscriptionOption> PeriodOptions { get; set; }

    ///<summary>
    /// Related user information
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
