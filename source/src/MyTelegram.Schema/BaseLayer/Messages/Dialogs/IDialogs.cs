// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Object contains a list of chats with messages and auxiliary data.
/// See <a href="https://corefork.telegram.org/constructor/messages.Dialogs" />
///</summary>
[JsonDerivedType(typeof(TDialogs), nameof(TDialogs))]
[JsonDerivedType(typeof(TDialogsSlice), nameof(TDialogsSlice))]
[JsonDerivedType(typeof(TDialogsNotModified), nameof(TDialogsNotModified))]
public interface IDialogs : IObject
{

}
