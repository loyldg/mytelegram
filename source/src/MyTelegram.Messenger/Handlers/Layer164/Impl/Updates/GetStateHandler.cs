// ReSharper disable All

namespace MyTelegram.Handlers.Updates;

///<summary>
/// Returns a current state of updates.
/// See <a href="https://corefork.telegram.org/method/updates.getState" />
///</summary>
internal sealed class GetStateHandler : RpcResultObjectHandler<MyTelegram.Schema.Updates.RequestGetState, MyTelegram.Schema.Updates.IState>,
    Updates.IGetStateHandler
{
    protected override Task<MyTelegram.Schema.Updates.IState> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Updates.RequestGetState obj)
    {
        throw new NotImplementedException();
    }
}
