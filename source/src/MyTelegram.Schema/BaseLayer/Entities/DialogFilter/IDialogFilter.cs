// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Dialog filter (<a href="https://corefork.telegram.org/api/folders">folder »</a>)
/// See <a href="https://corefork.telegram.org/constructor/DialogFilter" />
///</summary>
[JsonDerivedType(typeof(TDialogFilter), nameof(TDialogFilter))]
[JsonDerivedType(typeof(TDialogFilterDefault), nameof(TDialogFilterDefault))]
[JsonDerivedType(typeof(TDialogFilterChatlist), nameof(TDialogFilterChatlist))]
public interface IDialogFilter : IObject
{

}
