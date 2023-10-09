// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Create and upload a new <a href="https://corefork.telegram.org/api/wallpapers">wallpaper</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 WALLPAPER_FILE_INVALID The specified wallpaper file is invalid.
/// 400 WALLPAPER_MIME_INVALID The specified wallpaper MIME type is invalid.
/// See <a href="https://corefork.telegram.org/method/account.uploadWallPaper" />
///</summary>
internal sealed class UploadWallPaperHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUploadWallPaper, MyTelegram.Schema.IWallPaper>,
    Account.IUploadWallPaperHandler
{
    protected override Task<MyTelegram.Schema.IWallPaper> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUploadWallPaper obj)
    {
        throw new NotImplementedException();
    }
}
