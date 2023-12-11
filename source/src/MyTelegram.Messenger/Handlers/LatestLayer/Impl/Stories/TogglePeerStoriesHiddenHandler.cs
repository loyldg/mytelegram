// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.togglePeerStoriesHidden" />
///</summary>
internal sealed class TogglePeerStoriesHiddenHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestTogglePeerStoriesHidden, IBool>,
    Stories.ITogglePeerStoriesHiddenHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestTogglePeerStoriesHidden obj)
    {
        throw new NotImplementedException();
    }
}
