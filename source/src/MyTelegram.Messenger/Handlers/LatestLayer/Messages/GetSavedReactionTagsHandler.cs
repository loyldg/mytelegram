namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Fetch the full list of <a href="https://corefork.telegram.org/api/saved-messages#tags">saved message tags</a> created by the user.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getSavedReactionTags"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSavedReactionTagsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSavedReactionTags, MyTelegram.Schema.Messages.ISavedReactionTags>
{
    protected override Task<MyTelegram.Schema.Messages.ISavedReactionTags> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetSavedReactionTags obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.ISavedReactionTags>(new TSavedReactionTags { Tags = [] });
    }
}