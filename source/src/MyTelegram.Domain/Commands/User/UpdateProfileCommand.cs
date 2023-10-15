namespace MyTelegram.Domain.Commands.User;

public class UpdateProfileCommand : RequestCommand2<UserAggregate, UserId, IExecutionResult>
{
    public UpdateProfileCommand(UserId aggregateId,
        RequestInfo requestInfo,
        string? firstName,
        string? lastName,
        string? about) : base(aggregateId, requestInfo)
    {
        FirstName = firstName;
        LastName = lastName;
        About = about;
    }

    public string? About { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
}