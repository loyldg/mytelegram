namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Upload theme
/// Possible errors
/// Code Type Description
/// 400 THEME_FILE_INVALID Invalid theme file provided.
/// 400 THEME_MIME_INVALID The theme's MIME type is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.uploadTheme"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UploadThemeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUploadTheme, MyTelegram.Schema.IDocument>
{
    protected override Task<MyTelegram.Schema.IDocument> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestUploadTheme obj)
    {
        throw new NotImplementedException();
    }
}