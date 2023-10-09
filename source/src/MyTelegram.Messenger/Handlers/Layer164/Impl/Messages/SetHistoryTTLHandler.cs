// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Set maximum Time-To-Live of all messages in the specified chat
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 TTL_PERIOD_INVALID The specified TTL period is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.setHistoryTTL" />
///</summary>
internal sealed class SetHistoryTTLHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetHistoryTTL, MyTelegram.Schema.IUpdates>,
    Messages.ISetHistoryTTLHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSetHistoryTTL obj)
    {
        throw new NotImplementedException();
    }
}
