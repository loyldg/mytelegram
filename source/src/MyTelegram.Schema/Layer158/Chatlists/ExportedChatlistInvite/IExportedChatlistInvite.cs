// ReSharper disable All

namespace MyTelegram.Schema.Chatlists;

public interface IExportedChatlistInvite : IObject
{
    Schema.IDialogFilter Filter { get; set; }
    Schema.IExportedChatlistInvite Invite { get; set; }
}
