using MyTelegram.Schema.Updates;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Updates;

///<summary>
/// Returns a current state of updates.
/// See <a href="https://corefork.telegram.org/method/updates.getState" />
///</summary>
internal sealed class GetStateHandler(
    IPtsHelper ptsHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Updates.RequestGetState, MyTelegram.Schema.Updates.IState>
{
    protected override async Task<IState> HandleCoreAsync(IRequestInput input,
        RequestGetState obj)
    {
        if (input.UserId == 0)
        {
            RpcErrors.RpcErrors403.UserInvalid.ThrowRpcError();
            //RpcErrors.RpcErrors401.AuthKeyInvalid.ThrowRpcError();
        }

        var cacheItem = await ptsHelper.GetPtsForUserAsync(input.UserId);

        var state = new TState
        {
            Date = CurrentDate,
            Pts = cacheItem.Pts,
            Qts = cacheItem.Qts,
            Seq = 1,
            UnreadCount = cacheItem.UnreadCount,
        };

        return state;

    }
}
