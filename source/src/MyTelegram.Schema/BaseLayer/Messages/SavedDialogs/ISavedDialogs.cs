// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/messages.SavedDialogs" />
///</summary>
[JsonDerivedType(typeof(TSavedDialogs), nameof(TSavedDialogs))]
[JsonDerivedType(typeof(TSavedDialogsSlice), nameof(TSavedDialogsSlice))]
[JsonDerivedType(typeof(TSavedDialogsNotModified), nameof(TSavedDialogsNotModified))]
public interface ISavedDialogs : IObject
{

}
