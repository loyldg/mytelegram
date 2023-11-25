// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object defines a group profile photo.
/// See <a href="https://corefork.telegram.org/constructor/ChatPhoto" />
///</summary>
[JsonDerivedType(typeof(TChatPhotoEmpty), nameof(TChatPhotoEmpty))]
[JsonDerivedType(typeof(TChatPhoto), nameof(TChatPhoto))]
public interface IChatPhoto : IObject
{

}
