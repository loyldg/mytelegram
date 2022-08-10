namespace MyTelegram.ReadModel;

public interface IDialogFilterReadModel : IReadModel
{
    long OwnerUserId { get; }
    DialogFilter Filter { get; }
}
