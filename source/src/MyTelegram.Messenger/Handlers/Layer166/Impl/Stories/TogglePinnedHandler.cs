// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.togglePinned" />
///</summary>
internal sealed class TogglePinnedHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestTogglePinned, TVector<int>>,
    Stories.ITogglePinnedHandler
{
    protected override Task<TVector<int>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestTogglePinned obj)
    {
        throw new NotImplementedException();
    }
}
