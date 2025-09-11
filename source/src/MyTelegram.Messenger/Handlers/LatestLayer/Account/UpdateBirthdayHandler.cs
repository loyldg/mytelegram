namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Update our <a href="https://corefork.telegram.org/api/profile#birthday">birthday, see here »</a> for more info.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BIRTHDAY_INVALID An invalid age was specified, must be between 0 and 150 years.
/// See <a href="https://corefork.telegram.org/method/account.updateBirthday" />
///</summary>
internal sealed class UpdateBirthdayHandler(ICommandBus commandBus)
    : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateBirthday, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateBirthday obj)
    {
        Birthday? birthday = null;
        if (obj.Birthday != null)
        {
            birthday = new Birthday(obj.Birthday.Day, obj.Birthday.Month, obj.Birthday.Year);
        }

        var command = new UpdateBirthdayCommand(UserId.Create(input.UserId), birthday);
        await commandBus.PublishAsync(command);

        return new TBoolTrue();
    }
}
