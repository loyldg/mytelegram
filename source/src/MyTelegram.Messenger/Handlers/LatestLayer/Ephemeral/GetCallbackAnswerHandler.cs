
namespace MyTelegram.Messenger.Handlers.LatestLayer.Ephemeral;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetCallbackAnswerHandler : RpcResultObjectHandler<MyTelegram.Schema.Ephemeral.RequestGetCallbackAnswer, MyTelegram.Schema.Messages.IBotCallbackAnswer>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Messages.IBotCallbackAnswer> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Ephemeral.RequestGetCallbackAnswer obj)
    {
        throw new NotImplementedException();
    }
}

