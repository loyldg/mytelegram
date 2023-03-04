// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

internal sealed class GetEmojiProfilePhotoGroupsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiProfilePhotoGroups, MyTelegram.Schema.Messages.IEmojiGroups>,
    Messages.IGetEmojiProfilePhotoGroupsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IEmojiGroups> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetEmojiProfilePhotoGroups obj)
    {
        throw new NotImplementedException();
    }
}
