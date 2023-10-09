// ReSharper disable All

namespace MyTelegram.Handlers.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.getChatsToSend" />
///</summary>
internal sealed class GetChatsToSendHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetChatsToSend, MyTelegram.Schema.Messages.IChats>,
    Stories.IGetChatsToSendHandler
{
    protected override Task<MyTelegram.Schema.Messages.IChats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestGetChatsToSend obj)
    {
        throw new NotImplementedException();
    }
}
