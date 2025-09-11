namespace MyTelegram.Schema;

public interface ILayeredDraftMessage : IDraftMessage
{
    IInputMedia? Media { get; set; }
}