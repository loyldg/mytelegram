// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Suggested dialog filters (<a href="https://corefork.telegram.org/api/folders">folder »</a>)
/// See <a href="https://corefork.telegram.org/constructor/DialogFilterSuggested" />
///</summary>
[JsonDerivedType(typeof(TDialogFilterSuggested), nameof(TDialogFilterSuggested))]
public interface IDialogFilterSuggested : IObject
{
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/folders">Folder info</a>
    /// See <a href="https://corefork.telegram.org/type/DialogFilter" />
    ///</summary>
    MyTelegram.Schema.IDialogFilter Filter { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/folders">Folder</a> description
    ///</summary>
    string Description { get; set; }
}
