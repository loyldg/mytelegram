// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class TranslateTextHandler :
    RpcResultObjectHandler<Schema.Messages.RequestTranslateText, Schema.Messages.ITranslatedText>,
    Messages.ITranslateTextHandler, IProcessedHandler
{
    protected override Task<Schema.Messages.ITranslatedText> HandleCoreAsync(IRequestInput input,
        Schema.Messages.RequestTranslateText obj)
    {
        if (obj.Id?.Count > 0)
        {
            return Task.FromResult<Schema.Messages.ITranslatedText>(new TTranslateResult
            {
                Result = new TVector<ITextWithEntities>(obj.Id.Select(p => new TTextWithEntities
                {
                    Entities = new(),
                    Text =
                        $"The external translation API is not configured for MyTelegram,and the text will not be translated,ToLang:{obj.ToLang}"
                }))
            });
        }

        return Task.FromResult<Schema.Messages.ITranslatedText>(new TTranslateResult
        {
            Result = new TVector<ITextWithEntities>(new TTextWithEntities
            {
                Entities = new(),
                Text =
                    $"The external translation API is not configured for MyTelegram,and the text will not be translated,ToLang:{obj.ToLang}"
            })
        });
    }
}