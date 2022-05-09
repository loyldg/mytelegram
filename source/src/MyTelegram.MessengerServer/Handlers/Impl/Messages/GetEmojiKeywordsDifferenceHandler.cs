using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetEmojiKeywordsDifferenceHandler :
    RpcResultObjectHandler<RequestGetEmojiKeywordsDifference, IEmojiKeywordsDifference>,
    IGetEmojiKeywordsDifferenceHandler, IProcessedHandler
{
    protected override Task<IEmojiKeywordsDifference> HandleCoreAsync(IRequestInput input,
        RequestGetEmojiKeywordsDifference obj)
    {
        var r = new TEmojiKeywordsDifference {
            LangCode = "en", FromVersion = 0, Version = 0, Keywords = new TVector<IEmojiKeyword>()
        };

        return Task.FromResult<IEmojiKeywordsDifference>(r);
    }
}
