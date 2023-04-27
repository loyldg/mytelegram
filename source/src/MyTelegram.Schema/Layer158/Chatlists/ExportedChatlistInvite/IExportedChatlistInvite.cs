// ReSharper disable All

namespace MyTelegram.Schema.Chatlists;

public interface IExportedChatlistInvite : IObject
{
    MyTelegram.Schema.IDialogFilter Filter { get; set; }
    MyTelegram.Schema.IExportedChatlistInvite Invite { get; set; }
}
