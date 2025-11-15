namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Change default emoji reaction to use in the quick reaction menu: the value is synced across devices and can be fetched using <a href="https://corefork.telegram.org/method/help.getConfig">help.getConfig, <code>reactions_default</code> field</a>.
/// Possible errors
/// Code Type Description
/// 400 REACTION_INVALID The specified reaction is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.setDefaultReaction"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SetDefaultReactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetDefaultReaction, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSetDefaultReaction obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}