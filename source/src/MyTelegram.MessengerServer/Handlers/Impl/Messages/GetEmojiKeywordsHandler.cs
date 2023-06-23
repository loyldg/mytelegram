using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetEmojiKeywordsHandler : RpcResultObjectHandler<RequestGetEmojiKeywords, IEmojiKeywordsDifference>,
    IGetEmojiKeywordsHandler, IProcessedHandler
{
    protected override Task<IEmojiKeywordsDifference> HandleCoreAsync(IRequestInput input,
        RequestGetEmojiKeywords obj)
    {
        var r = new TEmojiKeywordsDifference
        {
            FromVersion = 0, Version = 0, LangCode = obj.LangCode, Keywords = new TVector<IEmojiKeyword>()
        };

        return Task.FromResult<IEmojiKeywordsDifference>(r);
    }
}