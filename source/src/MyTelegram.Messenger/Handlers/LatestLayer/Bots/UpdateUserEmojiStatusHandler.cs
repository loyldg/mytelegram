namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;

///<summary>
/// See <a href="https://corefork.telegram.org/method/bots.updateUserEmojiStatus" />
///</summary>
internal sealed class UpdateUserEmojiStatusHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestUpdateUserEmojiStatus, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestUpdateUserEmojiStatus obj)
    {
        throw new NotImplementedException();
    }
}
