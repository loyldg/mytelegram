// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Returns an HTTP URL which can be used to automatically log in into translation platform and suggest new <a href="https://corefork.telegram.org/api/custom-emoji#emoji-keywords">emoji keywords »</a>. The URL will be valid for 30 seconds after generation.
/// See <a href="https://corefork.telegram.org/method/messages.getEmojiURL" />
///</summary>
internal sealed class GetEmojiURLHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiURL, MyTelegram.Schema.IEmojiURL>,
    Messages.IGetEmojiURLHandler
{
    protected override Task<MyTelegram.Schema.IEmojiURL> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetEmojiURL obj)
    {
        throw new NotImplementedException();
    }
}
