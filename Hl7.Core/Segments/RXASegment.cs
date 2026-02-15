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
    [DataElement(1, "Give Sub-ID Counter", ElementUsage.Required)]
    public int GiveSubIdCounter { get; set; }

    [DataElement(2, "Administration Sub-ID Counter", ElementUsage.Required)]
    public string AdministrationSubIdCounter { get; set; } = string.Empty;

    [DataElement(3, "Date/Time Start of Administration", ElementUsage.Required)]
    public string DateTimeOfAdministration { get; set; } = string.Empty;

    [DataElement(4, "Date/Time End of Administration", ElementUsage.Required)]
    public string DateTimeOfAdministrationEnd { get; set; } = string.Empty;

    [DataElement(5, "Administered Code", ElementUsage.Required)]
    public string AdministeredCode { get; set; } = string.Empty;

    [DataElement(6, "Administered Amount", ElementUsage.Required)]
    public string AdministeredAmount { get; set; } = string.Empty;

    [DataElement(7, "Administered Units", ElementUsage.RequiredButMayBeEmpty)]
    public string AdministeredUnits { get; set; } = string.Empty;

    [DataElement(9, "Administration Notes", ElementUsage.RequiredButMayBeEmpty)]
    public string AdministrationNotes { get; set; } = string.Empty;

    [DataElement(10, "Administering Provider", ElementUsage.RequiredButMayBeEmpty)]
    public string AdministeringProvider { get; set; } = string.Empty;

    [DataElement(11, "Administered at Location", ElementUsage.RequiredButMayBeEmpty)]
    public string AdministeredAtLocation { get; set; } = string.Empty;

    [DataElement(12, "Administered Per (Time Unit)", ElementUsage.RequiredButMayBeEmpty)]
    public string AdministeredPer { get; set; } = string.Empty;

    [DataElement(13, "Administered Strength", ElementUsage.RequiredButMayBeEmpty)]
    public string AdministeredStrength { get; set; } = string.Empty;

    [DataElement(14, "Administered Strength Units", ElementUsage.RequiredButMayBeEmpty)]
    public string AdministeredStrengthUnits { get; set; } = string.Empty;

    [DataElement(15, "Substance Lot Number", ElementUsage.RequiredButMayBeEmpty)]
    public string SubstanceLotNumber { get; set; } = string.Empty;

    [DataElement(16, "Substance Expiration Date", ElementUsage.RequiredButMayBeEmpty)]
    public string SubstanceExpirationDate { get; set; } = string.Empty;

    [DataElement(17, "Substance Manufacturer Name", ElementUsage.RequiredButMayBeEmpty)]
    public string SubstanceManufacturerName { get; set; } = string.Empty;

    [DataElement(18, "Substance Refusal Reason", ElementUsage.Optional)]
    public string SubstanceRefusalReason { get; set; } = string.Empty;

    [DataElement(19, "Indication", ElementUsage.RequiredButMayBeEmpty)]
    public string Indication { get; set; } = string.Empty;

    [DataElement(20, "Completion Status", ElementUsage.RequiredButMayBeEmpty)]
    public string CompletionStatus { get; set; } = string.Empty;

    [DataElement(21, "Action Code", ElementUsage.RequiredButMayBeEmpty)]
    public string ActionCode { get; set; } = string.Empty;

    public RXASegment() : base("RXA")
    {
    }
}
