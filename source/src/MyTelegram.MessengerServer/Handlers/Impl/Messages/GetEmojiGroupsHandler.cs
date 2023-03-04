// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

internal sealed class GetEmojiGroupsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiGroups, MyTelegram.Schema.Messages.IEmojiGroups>,
    Messages.IGetEmojiGroupsHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.Messages.IEmojiGroups> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetEmojiGroups obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IEmojiGroups>(new TEmojiGroupsNotModified());
    }
}
