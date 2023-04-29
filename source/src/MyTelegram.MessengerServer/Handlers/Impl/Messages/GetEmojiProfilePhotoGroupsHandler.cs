// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

internal sealed class GetEmojiProfilePhotoGroupsHandler :
    RpcResultObjectHandler<RequestGetEmojiProfilePhotoGroups, IEmojiGroups>,
    Messages.IGetEmojiProfilePhotoGroupsHandler
{
    protected override Task<IEmojiGroups> HandleCoreAsync(IRequestInput input,
        RequestGetEmojiProfilePhotoGroups obj)
    {
        throw new NotImplementedException();
    }
}
