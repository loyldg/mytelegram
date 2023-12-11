// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Saves logs of application on the server.
/// See <a href="https://corefork.telegram.org/method/help.saveAppLog" />
///</summary>
internal sealed class SaveAppLogHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestSaveAppLog, IBool>,
    Help.ISaveAppLogHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestSaveAppLog obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
