// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// <a href="https://corefork.telegram.org/api/wallpapers">Wallpaper</a>
/// See <a href="https://corefork.telegram.org/constructor/InputWallPaper" />
///</summary>
[JsonDerivedType(typeof(TInputWallPaper), nameof(TInputWallPaper))]
[JsonDerivedType(typeof(TInputWallPaperSlug), nameof(TInputWallPaperSlug))]
[JsonDerivedType(typeof(TInputWallPaperNoFile), nameof(TInputWallPaperNoFile))]
public interface IInputWallPaper : IObject
{

}
