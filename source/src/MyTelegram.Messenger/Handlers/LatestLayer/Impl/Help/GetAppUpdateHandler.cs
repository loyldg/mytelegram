// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Returns information on update availability for the current application.
/// See <a href="https://corefork.telegram.org/method/help.getAppUpdate" />
///</summary>
internal sealed class GetAppUpdateHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetAppUpdate, MyTelegram.Schema.Help.IAppUpdate>,
    Help.IGetAppUpdateHandler
{
    protected override Task<MyTelegram.Schema.Help.IAppUpdate> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetAppUpdate obj)
    {
        return Task.FromResult<IAppUpdate>(new TNoAppUpdate());
    }
}
