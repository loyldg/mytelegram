namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Changes the privacy of already sent <a href="https://corefork.telegram.org/api/reactions#paid-reactions">paid reactions</a> on a specific message.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 REACTION_EMPTY Empty reaction provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.togglePaidReactionPrivacy"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class TogglePaidReactionPrivacyHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestTogglePaidReactionPrivacy, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestTogglePaidReactionPrivacy obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}