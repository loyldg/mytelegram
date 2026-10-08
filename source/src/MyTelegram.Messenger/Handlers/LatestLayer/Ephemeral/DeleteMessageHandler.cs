
namespace MyTelegram.Messenger.Handlers.LatestLayer.Ephemeral;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class DeleteMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Ephemeral.RequestDeleteMessage, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Ephemeral.RequestDeleteMessage obj)
    {
        throw new NotImplementedException();
    }
}

