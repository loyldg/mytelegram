// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a media with attached stickers
/// See <a href="https://corefork.telegram.org/constructor/InputStickeredMedia" />
///</summary>
[JsonDerivedType(typeof(TInputStickeredMediaPhoto), nameof(TInputStickeredMediaPhoto))]
[JsonDerivedType(typeof(TInputStickeredMediaDocument), nameof(TInputStickeredMediaDocument))]
public interface IInputStickeredMedia : IObject
{

}
