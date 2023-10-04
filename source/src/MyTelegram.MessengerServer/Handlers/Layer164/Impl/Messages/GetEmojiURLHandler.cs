using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetEmojiUrlHandler : RpcResultObjectHandler<RequestGetEmojiURL, IEmojiURL>,
    IGetEmojiURLHandler
{
    protected override Task<IEmojiURL> HandleCoreAsync(IRequestInput input,
        RequestGetEmojiURL obj)
    {
        throw new NotImplementedException();
    }
}