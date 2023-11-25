// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Cloud theme
/// See <a href="https://corefork.telegram.org/constructor/InputTheme" />
///</summary>
[JsonDerivedType(typeof(TInputTheme), nameof(TInputTheme))]
[JsonDerivedType(typeof(TInputThemeSlug), nameof(TInputThemeSlug))]
public interface IInputTheme : IObject
{

}
