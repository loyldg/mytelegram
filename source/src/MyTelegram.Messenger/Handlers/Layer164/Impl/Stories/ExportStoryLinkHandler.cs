// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.exportStoryLink" />
///</summary>
internal sealed class ExportStoryLinkHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestExportStoryLink, MyTelegram.Schema.IExportedStoryLink>,
    Stories.IExportStoryLinkHandler
{
    protected override Task<MyTelegram.Schema.IExportedStoryLink> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestExportStoryLink obj)
    {
        throw new NotImplementedException();
    }
}
