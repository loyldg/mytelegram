namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Generate a <a href="https://corefork.telegram.org/api/links#story-links">story deep link</a> for a specific story
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 STORY_ID_EMPTY You specified no story IDs.
/// 400 USER_PUBLIC_MISSING Cannot generate a link to stories posted by a peer without a username.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.exportStoryLink"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ExportStoryLinkHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestExportStoryLink, MyTelegram.Schema.IExportedStoryLink>
{
    protected override Task<MyTelegram.Schema.IExportedStoryLink> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestExportStoryLink obj)
    {
        throw new NotImplementedException();
    }
}