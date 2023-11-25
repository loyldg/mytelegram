// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Defines a photo for further interaction.
/// See <a href="https://corefork.telegram.org/constructor/InputPhoto" />
///</summary>
[JsonDerivedType(typeof(TInputPhotoEmpty), nameof(TInputPhotoEmpty))]
[JsonDerivedType(typeof(TInputPhoto), nameof(TInputPhoto))]
public interface IInputPhoto : IObject
{

}
