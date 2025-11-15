namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Set a <a href="https://corefork.telegram.org/api/custom-emoji">custom emoji stickerset</a> for supergroups. Only usable after reaching at least the <a href="https://corefork.telegram.org/api/boost">boost level »</a> specified in the <a href="https://corefork.telegram.org/api/config#group-emoji-stickers-level-min"><code>group_emoji_stickers_level_min</code> »</a> config parameter.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.setEmojiStickers"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SetEmojiStickersHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestSetEmojiStickers, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestSetEmojiStickers obj)
    {
        return Task.FromResult<IBool>(new MyTelegram.Schema.TBoolTrue());
    }
}