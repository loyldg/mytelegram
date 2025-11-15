namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get preview of webpage
/// Possible errors
/// Code Type Description
/// 400 ENTITY_BOUNDS_INVALID A specified <a href="https://corefork.telegram.org/api/entities#entity-length">entity offset or length</a> is invalid, see <a href="https://corefork.telegram.org/api/entities#entity-length">here »</a> for info on how to properly compute the entity offset/length.
/// 400 MESSAGE_EMPTY The provided message is empty.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getWebPagePreview"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetWebPagePreviewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetWebPagePreview, MyTelegram.Schema.Messages.IWebPagePreview>
{
    protected override Task<MyTelegram.Schema.Messages.IWebPagePreview> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetWebPagePreview obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IWebPagePreview>(new TWebPagePreview { Media = new TMessageMediaEmpty(), Users = [] });
    }
}