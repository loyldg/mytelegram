
namespace MyTelegram.Messenger.Handlers.Bots;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class CreateBotHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestCreateBot, MyTelegram.Schema.IUser>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IUser> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestCreateBot obj)
    {
        throw new NotImplementedException();
    }
}

