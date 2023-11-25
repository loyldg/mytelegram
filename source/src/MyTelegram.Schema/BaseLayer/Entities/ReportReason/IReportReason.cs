// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Report reason
/// See <a href="https://corefork.telegram.org/constructor/ReportReason" />
///</summary>
[JsonDerivedType(typeof(TInputReportReasonSpam), nameof(TInputReportReasonSpam))]
[JsonDerivedType(typeof(TInputReportReasonViolence), nameof(TInputReportReasonViolence))]
[JsonDerivedType(typeof(TInputReportReasonPornography), nameof(TInputReportReasonPornography))]
[JsonDerivedType(typeof(TInputReportReasonChildAbuse), nameof(TInputReportReasonChildAbuse))]
[JsonDerivedType(typeof(TInputReportReasonOther), nameof(TInputReportReasonOther))]
[JsonDerivedType(typeof(TInputReportReasonCopyright), nameof(TInputReportReasonCopyright))]
[JsonDerivedType(typeof(TInputReportReasonGeoIrrelevant), nameof(TInputReportReasonGeoIrrelevant))]
[JsonDerivedType(typeof(TInputReportReasonFake), nameof(TInputReportReasonFake))]
[JsonDerivedType(typeof(TInputReportReasonIllegalDrugs), nameof(TInputReportReasonIllegalDrugs))]
[JsonDerivedType(typeof(TInputReportReasonPersonalDetails), nameof(TInputReportReasonPersonalDetails))]
public interface IReportReason : IObject
{

}
