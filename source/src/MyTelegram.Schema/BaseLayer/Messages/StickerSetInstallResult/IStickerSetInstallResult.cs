// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Result of stickerset installation process
/// See <a href="https://corefork.telegram.org/constructor/messages.StickerSetInstallResult" />
///</summary>
[JsonDerivedType(typeof(TStickerSetInstallResultSuccess), nameof(TStickerSetInstallResultSuccess))]
[JsonDerivedType(typeof(TStickerSetInstallResultArchive), nameof(TStickerSetInstallResultArchive))]
public interface IStickerSetInstallResult : IObject
{

}
