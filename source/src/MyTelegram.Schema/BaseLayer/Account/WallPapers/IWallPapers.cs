// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// <a href="https://corefork.telegram.org/api/wallpapers">Wallpapers</a>
/// See <a href="https://corefork.telegram.org/constructor/account.WallPapers" />
///</summary>
[JsonDerivedType(typeof(TWallPapersNotModified), nameof(TWallPapersNotModified))]
[JsonDerivedType(typeof(TWallPapers), nameof(TWallPapers))]
public interface IWallPapers : IObject
{

}
