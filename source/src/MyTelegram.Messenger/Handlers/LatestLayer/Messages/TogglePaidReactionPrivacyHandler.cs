namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Changes the privacy of already sent <a href="https://corefork.telegram.org/api/reactions#paid-reactions">paid reactions</a> on a specific message.
/// See <a href="https://corefork.telegram.org/method/messages.togglePaidReactionPrivacy" />
///</summary>
internal sealed class TogglePaidReactionPrivacyHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestTogglePaidReactionPrivacy, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestTogglePaidReactionPrivacy obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
