namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Mark new featured stickers as read
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.readFeaturedStickers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReadFeaturedStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadFeaturedStickers, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestReadFeaturedStickers obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}