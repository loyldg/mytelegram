// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Contains info about the latest telegram Terms Of Service.
/// See <a href="https://corefork.telegram.org/constructor/help.TermsOfService" />
///</summary>
public interface ITermsOfService : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether a prompt must be showed to the user, in order to accept the new terms.
    ///</summary>
    bool Popup { get; set; }

    ///<summary>
    /// ID of the new terms
    /// See <a href="https://corefork.telegram.org/type/DataJSON" />
    ///</summary>
    MyTelegram.Schema.IDataJSON Id { get; set; }

    ///<summary>
    /// Text of the new terms
    ///</summary>
    string Text { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/entities">Message entities for styled text</a>
    /// See <a href="https://corefork.telegram.org/type/MessageEntity" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessageEntity> Entities { get; set; }

    ///<summary>
    /// Minimum age required to sign up to telegram, the user must confirm that they is older than the minimum age.
    ///</summary>
    int? MinAgeConfirm { get; set; }
}
