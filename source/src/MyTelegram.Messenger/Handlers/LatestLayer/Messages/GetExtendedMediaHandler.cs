namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Fetch updated information about <a href="https://corefork.telegram.org/api/paid-media">paid media, see here »</a> for the full flow.This method will return an array of <a href="https://corefork.telegram.org/constructor/updateMessageExtendedMedia">updateMessageExtendedMedia</a> updates, only for messages containing <strong>already bought</strong> paid media.<br>
/// No information will be returned for messages containing not yet bought paid media.
/// See <a href="https://corefork.telegram.org/method/messages.getExtendedMedia" />
///</summary>
internal sealed class GetExtendedMediaHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetExtendedMedia, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetExtendedMedia obj)
    {
        throw new NotImplementedException();
    }
}
