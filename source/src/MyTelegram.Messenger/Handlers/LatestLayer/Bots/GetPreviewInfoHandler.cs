namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Bot owners only, fetch <a href="https://corefork.telegram.org/api/bots/webapps#main-mini-app-previews">main mini app preview information, see here »</a> for more info.Note: technically non-owners may also invoke this method, but it will always behave exactly as <a href="https://corefork.telegram.org/method/bots.getPreviewMedias">bots.getPreviewMedias</a>, returning only previews for the current language and an empty <code>lang_codes</code> array, regardless of the passed <code>lang_code</code>, so please only use <a href="https://corefork.telegram.org/method/bots.getPreviewMedias">bots.getPreviewMedias</a> if you're not the owner of the <code>bot</code>.
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.getPreviewInfo"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetPreviewInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestGetPreviewInfo, MyTelegram.Schema.Bots.IPreviewInfo>
{
    protected override Task<MyTelegram.Schema.Bots.IPreviewInfo> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestGetPreviewInfo obj)
    {
        throw new NotImplementedException();
    }
}