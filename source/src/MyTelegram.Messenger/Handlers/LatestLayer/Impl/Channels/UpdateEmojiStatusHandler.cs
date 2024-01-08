// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// See <a href="https://corefork.telegram.org/method/channels.updateEmojiStatus" />
///</summary>
internal sealed class UpdateEmojiStatusHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestUpdateEmojiStatus, MyTelegram.Schema.IUpdates>,
    Channels.IUpdateEmojiStatusHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestUpdateEmojiStatus obj)
    {
        throw new NotImplementedException();
    }
}
