
namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetRichMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetRichMessage, MyTelegram.Schema.Messages.IMessages>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetRichMessage obj)
    {
        throw new NotImplementedException();
    }
}

