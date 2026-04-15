
namespace MyTelegram.Messenger.Handlers.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetFutureChatCreatorAfterLeaveHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetFutureChatCreatorAfterLeave, MyTelegram.Schema.IUser>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IUser> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetFutureChatCreatorAfterLeave obj)
    {
        throw new NotImplementedException();
    }
}

