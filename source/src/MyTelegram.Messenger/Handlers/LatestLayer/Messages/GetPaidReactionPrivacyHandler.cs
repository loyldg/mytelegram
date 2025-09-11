namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Fetches an <a href="https://corefork.telegram.org/constructor/updatePaidReactionPrivacy">updatePaidReactionPrivacy</a> update with the current <a href="https://corefork.telegram.org/api/reactions#paid-reactions">default paid reaction privacy, see here »</a> for more info.
/// See <a href="https://corefork.telegram.org/method/messages.getPaidReactionPrivacy" />
///</summary>
internal sealed class GetPaidReactionPrivacyHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPaidReactionPrivacy, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetPaidReactionPrivacy obj)
    {
        var update = new TUpdatePaidReactionPrivacy
        {
            Private = new TPaidReactionPrivacyDefault()
        };

        return Task.FromResult<MyTelegram.Schema.IUpdates>(new TUpdates
        {
            Updates = [update],
            Chats = [],
            Date = CurrentDate,
            Users = []
        });
    }
}
