namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

/// <summary>
/// RXA - Pharmacy/Vaccine Administration Segment
/// Used for recording vaccine administration details in CAIR2
/// </summary>
[Segment("RXA")]
public class RXASegment : Segment
{
    [DataElement(1)]
    public int GiveSubIdCounter { get; set; }

    [DataElement(2)]
    public string AdministrationSubIdCounter { get; set; } = string.Empty;

    [DataElement(3)]
    public string DateTimeOfAdministration { get; set; } = string.Empty;

    [DataElement(4)]
    public string DateTimeOfAdministrationEnd { get; set; } = string.Empty;

    [DataElement(5)]
    public string AdministeredCode { get; set; } = string.Empty;

    [DataElement(6)]
    public string AdministeredAmount { get; set; } = string.Empty;

    [DataElement(7)]
    public string AdministeredUnits { get; set; } = string.Empty;

    [DataElement(9)]
    public string AdministrationNotes { get; set; } = string.Empty;

    [DataElement(10)]
    public string AdministeringProvider { get; set; } = string.Empty;

    [DataElement(11)]
    public string AdministeredAtLocation { get; set; } = string.Empty;

    [DataElement(12)]
    public string AdministeredPer { get; set; } = string.Empty;

    [DataElement(13)]
    public string AdministeredStrength { get; set; } = string.Empty;

    [DataElement(14)]
    public string AdministeredStrengthUnits { get; set; } = string.Empty;

    [DataElement(15)]
    public string SubstanceLotNumber { get; set; } = string.Empty;

    [DataElement(16)]
    public string SubstanceExpirationDate { get; set; } = string.Empty;

    [DataElement(17)]
    public string SubstanceManufacturerName { get; set; } = string.Empty;

    [DataElement(18)]
    public string SubstanceRefusalReason { get; set; } = string.Empty;

    [DataElement(19)]
    public string Indication { get; set; } = string.Empty;

    [DataElement(20)]
    public string CompletionStatus { get; set; } = string.Empty;

    [DataElement(21)]
    public string ActionCode { get; set; } = string.Empty;

    public RXASegment() : base("RXA")
    {
    }
}
