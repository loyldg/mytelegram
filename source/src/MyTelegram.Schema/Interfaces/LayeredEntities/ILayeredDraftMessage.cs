namespace MyTelegram.Schema;

public partial interface ILayeredDraftMessage : IDraftMessage
{
    IInputMedia? Media { get; set; }
}