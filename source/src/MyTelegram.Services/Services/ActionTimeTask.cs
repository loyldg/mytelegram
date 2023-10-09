using HWT;
using Timeout = HWT.Timeout;

namespace MyTelegram.Services.Services;

public class ActionTimeTask : TimerTask
{
    private readonly Action _action;

    public ActionTimeTask(Action action)
    {
        _action = action;
    }

    public void Run(Timeout timeout)
    {
        _action();
    }
}