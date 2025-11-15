namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;
/// <summary>
/// Saves logs of application on the server.
/// <para><c>See <a href="https://corefork.telegram.org/method/help.saveAppLog"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✔]
/// </remarks>
internal sealed class SaveAppLogHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestSaveAppLog, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Help.RequestSaveAppLog obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}