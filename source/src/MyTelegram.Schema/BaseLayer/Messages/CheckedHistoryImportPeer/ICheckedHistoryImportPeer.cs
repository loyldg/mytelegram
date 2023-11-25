// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Contains a confirmation text to be shown to the user, upon <a href="https://corefork.telegram.org/api/import">importing chat history, click here for more info »</a>.
/// See <a href="https://corefork.telegram.org/constructor/messages.CheckedHistoryImportPeer" />
///</summary>
[JsonDerivedType(typeof(TCheckedHistoryImportPeer), nameof(TCheckedHistoryImportPeer))]
public interface ICheckedHistoryImportPeer : IObject
{
    ///<summary>
    /// A confirmation text to be shown to the user, upon <a href="https://corefork.telegram.org/api/import">importing chat history »</a>.
    ///</summary>
    string ConfirmText { get; set; }
}
