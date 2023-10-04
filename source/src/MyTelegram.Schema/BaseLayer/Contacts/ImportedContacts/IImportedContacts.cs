// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

///<summary>
/// Object contains info on successfully imported contacts.
/// See <a href="https://corefork.telegram.org/constructor/contacts.ImportedContacts" />
///</summary>
public interface IImportedContacts : IObject
{
    ///<summary>
    /// List of successfully imported contacts
    /// See <a href="https://corefork.telegram.org/type/ImportedContact" />
    ///</summary>
    TVector<MyTelegram.Schema.IImportedContact> Imported { get; set; }

    ///<summary>
    /// Popular contacts
    /// See <a href="https://corefork.telegram.org/type/PopularContact" />
    ///</summary>
    TVector<MyTelegram.Schema.IPopularContact> PopularInvites { get; set; }

    ///<summary>
    /// List of contact ids that could not be imported due to system limitation and will need to be imported at a later date.
    ///</summary>
    TVector<long> RetryContacts { get; set; }

    ///<summary>
    /// List of users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
