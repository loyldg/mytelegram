namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.getAvailableEffects" />
///</summary>
internal sealed class GetAvailableEffectsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAvailableEffects, MyTelegram.Schema.Messages.IAvailableEffects>
{
    protected override Task<MyTelegram.Schema.Messages.IAvailableEffects> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetAvailableEffects obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IAvailableEffects>(new TAvailableEffects
        {
            Documents = [],
            Effects = [],
        });
    }
}
