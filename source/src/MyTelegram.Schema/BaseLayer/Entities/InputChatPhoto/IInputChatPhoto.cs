// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Defines a new group profile photo.
/// See <a href="https://corefork.telegram.org/constructor/InputChatPhoto" />
///</summary>
[JsonDerivedType(typeof(TInputChatPhotoEmpty), nameof(TInputChatPhotoEmpty))]
[JsonDerivedType(typeof(TInputChatUploadedPhoto), nameof(TInputChatUploadedPhoto))]
[JsonDerivedType(typeof(TInputChatPhoto), nameof(TInputChatPhoto))]
public interface IInputChatPhoto : IObject
{

}
