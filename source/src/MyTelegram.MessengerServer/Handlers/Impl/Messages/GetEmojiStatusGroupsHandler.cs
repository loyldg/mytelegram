// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

internal sealed class GetEmojiStatusGroupsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiStatusGroups, MyTelegram.Schema.Messages.IEmojiGroups>,
    Messages.IGetEmojiStatusGroupsHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.Messages.IEmojiGroups> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetEmojiStatusGroups obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IEmojiGroups>(new TEmojiGroups
        {
            Groups = new()
        });
    }
}
