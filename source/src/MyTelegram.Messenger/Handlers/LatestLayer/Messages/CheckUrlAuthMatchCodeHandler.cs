
namespace MyTelegram.Messenger.Handlers.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class CheckUrlAuthMatchCodeHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestCheckUrlAuthMatchCode, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestCheckUrlAuthMatchCode obj)
    {
        throw new NotImplementedException();
    }
}

