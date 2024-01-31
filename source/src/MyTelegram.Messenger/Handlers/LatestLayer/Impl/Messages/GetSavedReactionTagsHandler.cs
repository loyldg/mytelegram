// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.getSavedReactionTags" />
///</summary>
internal sealed class GetSavedReactionTagsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSavedReactionTags, MyTelegram.Schema.Messages.ISavedReactionTags>,
    Messages.IGetSavedReactionTagsHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.Messages.ISavedReactionTags> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetSavedReactionTags obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.ISavedReactionTags>(new TSavedReactionTags
        {
            Tags = new()
        });
    }
}
