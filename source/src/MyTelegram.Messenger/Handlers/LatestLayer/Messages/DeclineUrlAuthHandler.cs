
namespace MyTelegram.Messenger.Handlers.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class DeclineUrlAuthHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeclineUrlAuth, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestDeclineUrlAuth obj)
    {
        throw new NotImplementedException();
    }
}

