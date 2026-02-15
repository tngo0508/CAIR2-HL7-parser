namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

/// <summary>
/// PID - Patient Identification Segment
/// </summary>
[Segment("PID")]
public class PIDSegment : Segment
{
    [DataElement(1, "Set ID - PID", ElementUsage.Optional)]
    public int SetId { get; set; }

    [DataElement(2, "Patient ID", ElementUsage.Optional)]
    public string PatientId { get; set; } = string.Empty;

    [DataElement(3, "Patient Identifier List", ElementUsage.Optional)]
    public string PatientIdentifierList { get; set; } = string.Empty;

    [DataElement(4, "Alternate Patient ID - PID", ElementUsage.Optional)]
    public string AlternatePatientId { get; set; } = string.Empty;

    [DataElement(5, "Patient Name", ElementUsage.Optional)]
    public string PatientName { get; set; } = string.Empty;

    [DataElement(6, "Mother's Maiden Name", ElementUsage.Optional)]
    public string MothersMaidenName { get; set; } = string.Empty;

    [DataElement(7, "Date/Time of Birth", ElementUsage.Optional)]
    public string DateOfBirth { get; set; } = string.Empty;

    [DataElement(8, "Administrative Sex", ElementUsage.Optional)]
    public string AdministrativeSex { get; set; } = string.Empty;

    [DataElement(10, "Race", ElementUsage.Optional)]
    public string Race { get; set; } = string.Empty;

    [DataElement(11, "Patient Address", ElementUsage.Optional)]
    public string PatientAddress { get; set; } = string.Empty;

    [DataElement(12, "County Code", ElementUsage.Optional)]
    public string CountyCode { get; set; } = string.Empty;

    [DataElement(13, "Phone Number - Home", ElementUsage.Optional)]
    public string PhoneNumberHome { get; set; } = string.Empty;

    [DataElement(14, "Phone Number - Business", ElementUsage.Optional)]
    public string PhoneNumberBusiness { get; set; } = string.Empty;

    [DataElement(15, "Primary Language", ElementUsage.Optional)]
    public string PrimaryLanguage { get; set; } = string.Empty;

    public PIDSegment() : base("PID")
    {
    }
}
