namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Reorder active usernames
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.reorderUsernames"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReorderUsernamesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestReorderUsernames, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestReorderUsernames obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}