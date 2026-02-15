namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("QPD")]
public class QPDSegment : Segment
{
    [DataElement(1)]
    public string MessageQueryName { get; set; } = string.Empty;

    [DataElement(2)]
    public string QueryTag { get; set; } = string.Empty;

    [DataElement(3)]
    public string PatientIdentifierList { get; set; } = string.Empty;

    [DataElement(4)]
    public string PatientName { get; set; } = string.Empty;

    [DataElement(5)]
    public string MothersMaidenName { get; set; } = string.Empty;

    [DataElement(6)]
    public string DateOfBirth { get; set; } = string.Empty;

    [DataElement(7)]
    public string AdministrativeSex { get; set; } = string.Empty;

    [DataElement(8)]
    public string PatientAddress { get; set; } = string.Empty;

    [DataElement(9)]
    public string PhoneNumberHome { get; set; } = string.Empty;

    [DataElement(10)]
    public string MultipleBirthIndicator { get; set; } = string.Empty;

    [DataElement(11)]
    public string BirthOrder { get; set; } = string.Empty;

    public QPDSegment() : base("QPD")
    {
    }
}
