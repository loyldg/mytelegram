// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Updates user profile.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ABOUT_TOO_LONG About string too long.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 FIRSTNAME_INVALID The first name is invalid.
/// See <a href="https://corefork.telegram.org/method/account.updateProfile" />
///</summary>
internal sealed class UpdateProfileHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateProfile, MyTelegram.Schema.IUser>,
    Account.IUpdateProfileHandler
{
    private readonly ICommandBus _commandBus;

    public UpdateProfileHandler(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    protected override async Task<IUser> HandleCoreAsync(IRequestInput input,
        RequestUpdateProfile obj)
    {
        var userId = UserId.Create(input.UserId);
        var command = new UpdateProfileCommand(userId,
            input.ToRequestInfo(),
            obj.FirstName,
            obj.LastName,
            obj.About);
        await _commandBus.PublishAsync(command, CancellationToken.None);

        return null!;
    }
}
