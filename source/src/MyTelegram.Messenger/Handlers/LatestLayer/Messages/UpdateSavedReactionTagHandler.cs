namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Update the <a href="https://corefork.telegram.org/api/saved-messages#tags">description of a saved message tag »</a>.
/// Possible errors
/// Code Type Description
/// 403 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// 400 REACTION_INVALID The specified reaction is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.updateSavedReactionTag"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateSavedReactionTagHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUpdateSavedReactionTag, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestUpdateSavedReactionTag obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}