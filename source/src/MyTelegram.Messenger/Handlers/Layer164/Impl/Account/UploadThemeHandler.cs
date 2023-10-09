// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Upload theme
/// <para>Possible errors</para>
/// Code Type Description
/// 400 THEME_FILE_INVALID Invalid theme file provided.
/// See <a href="https://corefork.telegram.org/method/account.uploadTheme" />
///</summary>
internal sealed class UploadThemeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUploadTheme, MyTelegram.Schema.IDocument>,
    Account.IUploadThemeHandler
{
    protected override Task<MyTelegram.Schema.IDocument> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUploadTheme obj)
    {
        throw new NotImplementedException();
    }
}
