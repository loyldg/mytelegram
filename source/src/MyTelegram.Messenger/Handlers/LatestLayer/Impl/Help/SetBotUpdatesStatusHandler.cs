// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Informs the server about the number of pending bot updates if they haven't been processed for a long time; for bots only
/// See <a href="https://corefork.telegram.org/method/help.setBotUpdatesStatus" />
///</summary>
internal sealed class SetBotUpdatesStatusHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestSetBotUpdatesStatus, IBool>,
    Help.ISetBotUpdatesStatusHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestSetBotUpdatesStatus obj)
    {
        throw new NotImplementedException();
    }
}
