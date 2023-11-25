// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Saved GIFs
/// See <a href="https://corefork.telegram.org/constructor/messages.SavedGifs" />
///</summary>
[JsonDerivedType(typeof(TSavedGifsNotModified), nameof(TSavedGifsNotModified))]
[JsonDerivedType(typeof(TSavedGifs), nameof(TSavedGifs))]
public interface ISavedGifs : IObject
{

}
