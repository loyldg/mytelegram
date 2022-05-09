namespace MyTelegram.Core;

public interface IScheduleAppService
{
    long Execute(Action action,
        TimeSpan timeSpan);

    Task ExecuteAsync(Action action,
        TimeSpan timeSpan);
}