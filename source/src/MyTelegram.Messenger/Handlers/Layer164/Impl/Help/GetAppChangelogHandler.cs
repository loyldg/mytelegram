// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Get changelog of current app.<br>
/// Typically, an <a href="https://corefork.telegram.org/constructor/updates">updates</a> constructor will be returned, containing one or more <a href="https://corefork.telegram.org/constructor/updateServiceNotification">updateServiceNotification</a> updates with app-specific changelogs.
/// See <a href="https://corefork.telegram.org/method/help.getAppChangelog" />
///</summary>
internal sealed class GetAppChangelogHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetAppChangelog, MyTelegram.Schema.IUpdates>,
    Help.IGetAppChangelogHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetAppChangelog obj)
    {
        throw new NotImplementedException();
    }
}
