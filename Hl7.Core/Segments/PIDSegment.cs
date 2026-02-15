namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

/// <summary>
/// PID - Patient Identification Segment
/// </summary>
[Segment("PID")]
public class PIDSegment : Segment
{
    [DataElement(1)]
    public int SetId { get; set; }

    [DataElement(2)]
    public string PatientId { get; set; } = string.Empty;

    [DataElement(3)]
    public string PatientIdentifierList { get; set; } = string.Empty;

    [DataElement(4)]
    public string AlternatePatientId { get; set; } = string.Empty;

    [DataElement(5)]
    public string PatientName { get; set; } = string.Empty;

    [DataElement(6)]
    public string MothersMaidenName { get; set; } = string.Empty;

    [DataElement(7)]
    public string DateOfBirth { get; set; } = string.Empty;

    [DataElement(8)]
    public string AdministrativeSex { get; set; } = string.Empty;

    [DataElement(9)]
    public string Race { get; set; } = string.Empty;

    [DataElement(10)]
    public string PatientAddress { get; set; } = string.Empty;

    [DataElement(11)]
    public string CountyCode { get; set; } = string.Empty;

    [DataElement(12)]
    public string PhoneNumberHome { get; set; } = string.Empty;

    [DataElement(13)]
    public string PhoneNumberBusiness { get; set; } = string.Empty;

    [DataElement(14)]
    public string PrimaryLanguage { get; set; } = string.Empty;

    public PIDSegment() : base("PID")
    {
    }
}
