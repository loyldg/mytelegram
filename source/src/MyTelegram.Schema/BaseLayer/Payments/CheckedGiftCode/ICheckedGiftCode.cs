// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Info about a <a href="https://corefork.telegram.org/api/giveaways">Telegram Premium Giftcode</a>.
/// See <a href="https://corefork.telegram.org/constructor/payments.CheckedGiftCode" />
///</summary>
[JsonDerivedType(typeof(TCheckedGiftCode), nameof(TCheckedGiftCode))]
public interface ICheckedGiftCode : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether this giftcode was created by a <a href="https://corefork.telegram.org/api/giveaways">giveaway</a>.
    ///</summary>
    bool ViaGiveaway { get; set; }

    ///<summary>
    /// The peer that created the gift code.
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer? FromId { get; set; }

    ///<summary>
    /// Message ID of the giveaway in the channel specified in <code>from_id</code>.
    ///</summary>
    int? GiveawayMsgId { get; set; }

    ///<summary>
    /// The destination user of the gift.
    ///</summary>
    long? ToId { get; set; }

    ///<summary>
    /// Creation date of the gift code.
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// Duration in months of the gifted <a href="https://corefork.telegram.org/api/premium">Telegram Premium</a> subscription.
    ///</summary>
    int Months { get; set; }

    ///<summary>
    /// When was the giftcode imported, if it was imported.
    ///</summary>
    int? UsedDate { get; set; }

    ///<summary>
    /// Mentioned chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Mentioned users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
