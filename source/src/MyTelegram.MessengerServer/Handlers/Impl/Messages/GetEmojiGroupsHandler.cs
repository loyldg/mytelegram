// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

internal sealed class GetEmojiGroupsHandler :
    RpcResultObjectHandler<Schema.Messages.RequestGetEmojiGroups, Schema.Messages.IEmojiGroups>,
    Messages.IGetEmojiGroupsHandler, IProcessedHandler
{
    protected override Task<Schema.Messages.IEmojiGroups> HandleCoreAsync(IRequestInput input,
        Schema.Messages.RequestGetEmojiGroups obj)
    {
        return Task.FromResult<Schema.Messages.IEmojiGroups>(new TEmojiGroupsNotModified());
    }
}
