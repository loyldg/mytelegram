namespace MyTelegram.Messenger.Handlers.LatestLayer.Stickers;
/// <summary>
/// Suggests a short name for a given stickerpack name
/// Possible errors
/// Code Type Description
/// 400 TITLE_INVALID The specified stickerpack title is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stickers.suggestShortName"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SuggestShortNameHandler : RpcResultObjectHandler<MyTelegram.Schema.Stickers.RequestSuggestShortName, MyTelegram.Schema.Stickers.ISuggestedShortName>
{
    protected override Task<MyTelegram.Schema.Stickers.ISuggestedShortName> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stickers.RequestSuggestShortName obj)
    {
        throw new NotImplementedException();
    }
}