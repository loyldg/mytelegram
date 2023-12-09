// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get a list of <a href="https://corefork.telegram.org/api/channel">channels/supergroups</a> we left
/// <para>Possible errors</para>
/// Code Type Description
/// 403 TAKEOUT_REQUIRED A takeout session has to be initialized, first.
/// See <a href="https://corefork.telegram.org/method/channels.getLeftChannels" />
///</summary>
internal sealed class GetLeftChannelsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetLeftChannels, MyTelegram.Schema.Messages.IChats>,
    Channels.IGetLeftChannelsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IChats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetLeftChannels obj)
    {
        throw new NotImplementedException();
    }
}
