using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetEmojiKeywordsLanguagesHandler :
    RpcResultObjectHandler<RequestGetEmojiKeywordsLanguages, TVector<IEmojiLanguage>>,
    IGetEmojiKeywordsLanguagesHandler, IProcessedHandler
{
    protected override Task<TVector<IEmojiLanguage>> HandleCoreAsync(IRequestInput input,
        RequestGetEmojiKeywordsLanguages obj)
    {
        IEmojiLanguage[] languages = { new TEmojiLanguage { LangCode = "en" }, new TEmojiLanguage { LangCode = "zh-hans" } };

        var r = new TVector<IEmojiLanguage>(languages);

        return Task.FromResult(r);
    }
}
