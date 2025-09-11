namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Notifies the sender about the recipient having listened a voice message or watched a video.
/// See <a href="https://corefork.telegram.org/method/messages.readMessageContents" />
///</summary>
internal sealed class ReadMessageContentsHandler(IPtsHelper ptsHelper) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadMessageContents, MyTelegram.Schema.Messages.IAffectedMessages>
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReadMessageContents obj)
    {
        return System.Threading.Tasks.Task.FromResult<MyTelegram.Schema.Messages.IAffectedMessages>(
            new TAffectedMessages
            {
                Pts = ptsHelper.GetCachedPts(input.UserId),
                PtsCount = 0
            });
    }
}
