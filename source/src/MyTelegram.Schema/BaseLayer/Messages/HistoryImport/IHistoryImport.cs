// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Identifier of a <a href="https://corefork.telegram.org/api/import">history import session, click here for more info »</a>.
/// See <a href="https://corefork.telegram.org/constructor/messages.HistoryImport" />
///</summary>
[JsonDerivedType(typeof(THistoryImport), nameof(THistoryImport))]
public interface IHistoryImport : IObject
{
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/import">History import ID</a>
    ///</summary>
    long Id { get; set; }
}
