// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class TranslateTextHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestTranslateText, MyTelegram.Schema.Messages.ITranslatedText>,
    Messages.ITranslateTextHandler
{
    protected override Task<MyTelegram.Schema.Messages.ITranslatedText> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestTranslateText obj)
    {
        throw new NotImplementedException();
    }
}
