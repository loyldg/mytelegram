namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Associate a stickerset to the supergroup
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PARTICIPANTS_TOO_FEW Not enough participants.
/// 406 STICKERSET_OWNER_ANONYMOUS Provided stickerset can't be installed as group stickerset to prevent admin deanonymization.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.setStickers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class SetStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestSetStickers, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestSetStickers obj)
    {
        return Task.FromResult<IBool>(new MyTelegram.Schema.TBoolTrue());
    }
}