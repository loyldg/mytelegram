// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Gets the default value of the Time-To-Live setting, applied to all new chats.
/// See <a href="https://corefork.telegram.org/method/messages.getDefaultHistoryTTL" />
///</summary>
internal sealed class GetDefaultHistoryTTLHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDefaultHistoryTTL, MyTelegram.Schema.IDefaultHistoryTTL>,
    Messages.IGetDefaultHistoryTTLHandler
{
    protected override Task<MyTelegram.Schema.IDefaultHistoryTTL> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetDefaultHistoryTTL obj)
    {
        throw new NotImplementedException();
    }
}
