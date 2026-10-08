
using MyTelegram.Schema.Ephemeral;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Ephemeral;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetWelcomeMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Ephemeral.RequestGetWelcomeMessages, MyTelegram.Schema.Ephemeral.IWelcomeMessages>
{
    protected override Task<MyTelegram.Schema.Ephemeral.IWelcomeMessages> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Ephemeral.RequestGetWelcomeMessages obj)
    {
        return Task.FromResult<IWelcomeMessages>(new TWelcomeMessages
        {
            Messages = []
        });
    }
}

