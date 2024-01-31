// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/messages.SavedReactionTags" />
///</summary>
[JsonDerivedType(typeof(TSavedReactionTagsNotModified), nameof(TSavedReactionTagsNotModified))]
[JsonDerivedType(typeof(TSavedReactionTags), nameof(TSavedReactionTags))]
public interface ISavedReactionTags : IObject
{

}
