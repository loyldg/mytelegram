namespace MyTelegram.Domain.Commands.User;

public class UpdateProfileCommand : RequestCommand<UserAggregate, UserId, IExecutionResult>
{
    public UpdateProfileCommand(UserId aggregateId,
        long reqMsgId,
        string? firstName,
        string? lastName,
        string? about) : base(aggregateId, reqMsgId)
    {
        FirstName = firstName;
        LastName = lastName;
        About = about;
    }

    public string? About { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
}
