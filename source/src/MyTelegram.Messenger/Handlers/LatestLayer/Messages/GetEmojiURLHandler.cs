namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Returns an HTTP URL which can be used to automatically log in into translation platform and suggest new <a href="https://corefork.telegram.org/api/custom-emoji#emoji-keywords">emoji keywords »</a>. The URL will be valid for 30 seconds after generation.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getEmojiURL"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetEmojiURLHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiURL, MyTelegram.Schema.IEmojiURL>
{
    protected override Task<MyTelegram.Schema.IEmojiURL> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetEmojiURL obj)
    {
        throw new NotImplementedException();
    }
}