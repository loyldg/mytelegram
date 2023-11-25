// ReSharper disable All

namespace MyTelegram.Schema.Phone;

///<summary>
/// An exported group call invitation.
/// See <a href="https://corefork.telegram.org/constructor/phone.ExportedGroupCallInvite" />
///</summary>
[JsonDerivedType(typeof(TExportedGroupCallInvite), nameof(TExportedGroupCallInvite))]
public interface IExportedGroupCallInvite : IObject
{
    ///<summary>
    /// Invite link
    ///</summary>
    string Link { get; set; }
}
