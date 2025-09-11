namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;

///<summary>
/// Fetch <a href="https://corefork.telegram.org/api/bots/webapps#main-mini-app-previews">main mini app previews, see here »</a> for more info.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// See <a href="https://corefork.telegram.org/method/bots.getPreviewMedias" />
///</summary>
internal sealed class GetPreviewMediasHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestGetPreviewMedias, TVector<MyTelegram.Schema.IBotPreviewMedia>>
{
    protected override Task<TVector<MyTelegram.Schema.IBotPreviewMedia>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestGetPreviewMedias obj)
    {
        throw new NotImplementedException();
    }
}
