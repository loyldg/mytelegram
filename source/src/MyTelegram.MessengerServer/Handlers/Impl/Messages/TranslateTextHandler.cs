// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class TranslateTextHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestTranslateText, MyTelegram.Schema.Messages.ITranslatedText>,
    Messages.ITranslateTextHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.Messages.ITranslatedText> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestTranslateText obj)
    {
        if (obj.Id?.Count > 0)
        {

            return Task.FromResult<MyTelegram.Schema.Messages.ITranslatedText>(new TTranslateResult
            {
                Result = new TVector<ITextWithEntities>(obj.Id.Select(p => new TTextWithEntities
                {
                    Entities = new(),
                    Text =
                        $"The external translation API is not configured for MyTelegram,and the text will not be translated,ToLang:{obj.ToLang}"
                }))
            });
        }

        return Task.FromResult<MyTelegram.Schema.Messages.ITranslatedText>(new TTranslateResult
        {
            Result = new TVector<ITextWithEntities>(new TTextWithEntities
            {
                Entities = new(),
                Text = $"The external translation API is not configured for MyTelegram,and the text will not be translated,ToLang:{obj.ToLang}"
            })
        });
    }
}
