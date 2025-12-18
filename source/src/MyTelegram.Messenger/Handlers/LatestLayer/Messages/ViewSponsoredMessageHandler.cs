namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Mark a specific <a href="https://corefork.telegram.org/api/sponsored-messages">sponsored message »</a> as read
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.viewSponsoredMessage"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ViewSponsoredMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestViewSponsoredMessage, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestViewSponsoredMessage obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}