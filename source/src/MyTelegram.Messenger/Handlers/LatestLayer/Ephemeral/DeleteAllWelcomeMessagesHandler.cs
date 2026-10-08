
namespace MyTelegram.Messenger.Handlers.LatestLayer.Ephemeral;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class DeleteAllWelcomeMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Ephemeral.RequestDeleteAllWelcomeMessages, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Ephemeral.RequestDeleteAllWelcomeMessages obj)
    {
        throw new NotImplementedException();
    }
}

