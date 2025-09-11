namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Reorder <a href="https://corefork.telegram.org/api/business#quick-reply-shortcuts">quick reply shortcuts</a>.This will emit an <a href="https://corefork.telegram.org/constructor/updateQuickReplies">updateQuickReplies</a> update to other logged-in sessions.
/// <para>Possible errors</para>
/// Code Type Description
/// 403 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// See <a href="https://corefork.telegram.org/method/messages.reorderQuickReplies" />
///</summary>
internal sealed class ReorderQuickRepliesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReorderQuickReplies, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReorderQuickReplies obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
