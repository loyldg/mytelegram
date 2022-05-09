namespace MyTelegram.MessengerServer.Services.Interfaces;

public interface IDialogAppService
{
    Task<GetDialogOutput> GetDialogsAsync(GetDialogInput input);
    Task ReorderPinnedDialogsAsync(ReorderPinnedDialogsInput input);
}