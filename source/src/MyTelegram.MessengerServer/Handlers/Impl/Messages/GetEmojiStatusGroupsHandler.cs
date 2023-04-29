// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

internal sealed class GetEmojiStatusGroupsHandler : RpcResultObjectHandler<Schema.Messages.RequestGetEmojiStatusGroups,
        Schema.Messages.IEmojiGroups>,
    Messages.IGetEmojiStatusGroupsHandler, IProcessedHandler
{
    protected override Task<Schema.Messages.IEmojiGroups> HandleCoreAsync(IRequestInput input,
        Schema.Messages.RequestGetEmojiStatusGroups obj)
    {
        return Task.FromResult<Schema.Messages.IEmojiGroups>(new TEmojiGroups
        {
            Groups = new()
        });
    }
}
