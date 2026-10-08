
namespace MyTelegram.Messenger.Handlers.LatestLayer.Ephemeral;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class DeleteWelcomeMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Ephemeral.RequestDeleteWelcomeMessage, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Ephemeral.RequestDeleteWelcomeMessage obj)
    {
        throw new NotImplementedException();
    }
}

