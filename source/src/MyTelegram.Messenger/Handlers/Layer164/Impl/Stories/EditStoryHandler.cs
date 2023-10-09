// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.editStory" />
///</summary>
internal sealed class EditStoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestEditStory, MyTelegram.Schema.IUpdates>,
    Stories.IEditStoryHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestEditStory obj)
    {
        throw new NotImplementedException();
    }
}
