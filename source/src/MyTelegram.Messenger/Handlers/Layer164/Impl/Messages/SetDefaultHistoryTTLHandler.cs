// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Changes the default value of the Time-To-Live setting, applied to all new chats.
/// See <a href="https://corefork.telegram.org/method/messages.setDefaultHistoryTTL" />
///</summary>
internal sealed class SetDefaultHistoryTTLHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetDefaultHistoryTTL, IBool>,
    Messages.ISetDefaultHistoryTTLHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSetDefaultHistoryTTL obj)
    {
        throw new NotImplementedException();
    }
}
