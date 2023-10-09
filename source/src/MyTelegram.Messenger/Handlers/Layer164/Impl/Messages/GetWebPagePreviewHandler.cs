// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get preview of webpage
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ENTITY_BOUNDS_INVALID A specified <a href="https://corefork.telegram.org/api/entities#entity-length">entity offset or length</a> is invalid, see <a href="https://corefork.telegram.org/api/entities#entity-length">here »</a> for info on how to properly compute the entity offset/length.
/// 400 MESSAGE_EMPTY The provided message is empty.
/// See <a href="https://corefork.telegram.org/method/messages.getWebPagePreview" />
///</summary>
internal sealed class GetWebPagePreviewHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetWebPagePreview, MyTelegram.Schema.IMessageMedia>,
    Messages.IGetWebPagePreviewHandler
{
    protected override Task<MyTelegram.Schema.IMessageMedia> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetWebPagePreview obj)
    {
        throw new NotImplementedException();
    }
}
