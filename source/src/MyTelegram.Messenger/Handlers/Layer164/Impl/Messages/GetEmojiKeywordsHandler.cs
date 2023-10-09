// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get localized <a href="https://corefork.telegram.org/api/custom-emoji#emoji-keywords">emoji keywords »</a>.
/// See <a href="https://corefork.telegram.org/method/messages.getEmojiKeywords" />
///</summary>
internal sealed class GetEmojiKeywordsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiKeywords, MyTelegram.Schema.IEmojiKeywordsDifference>,
    Messages.IGetEmojiKeywordsHandler
{
    protected override Task<MyTelegram.Schema.IEmojiKeywordsDifference> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetEmojiKeywords obj)
    {
        throw new NotImplementedException();
    }
}
