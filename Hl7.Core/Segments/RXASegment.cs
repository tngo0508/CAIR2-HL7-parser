using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;
using Hl7.Core.Types;

namespace Hl7.Core.Segments;

/// <summary>
/// RXA - Pharmacy/Vaccine Administration Segment
/// Used for recording vaccine administration details in CAIR2
/// </summary>
[Segment("RXA")]
public class RXASegment : Segment
{
    /// <summary>
    /// Gets or sets the Give Sub-ID Counter (RXA-1)
    /// </summary>
    [DataElement(1, "Give Sub-ID Counter", ElementUsage.Required)]
    public int GiveSubIdCounter { get; set; }

    /// <summary>
    /// Gets or sets the Administration Sub-ID Counter (RXA-2)
    /// </summary>
    [DataElement(2, "Administration Sub-ID Counter", ElementUsage.Required)]
    public string AdministrationSubIdCounter { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Date/Time Start of Administration (RXA-3)
    /// </summary>
    [DataElement(3, "Date/Time Start of Administration", ElementUsage.Required)]
    public string DateTimeOfAdministration { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Date/Time End of Administration (RXA-4)
    /// </summary>
    [DataElement(4, "Date/Time End of Administration", ElementUsage.Required)]
    public string DateTimeOfAdministrationEnd { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Administered Code (RXA-5)
    /// </summary>
    [DataElement(5, "Administered Code", ElementUsage.Required)]
    public CE AdministeredCode { get; set; } = new();

    /// <summary>
    /// Gets or sets the Administered Amount (RXA-6)
    /// </summary>
    [DataElement(6, "Administered Amount", ElementUsage.Required)]
    public string AdministeredAmount { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Administered Units (RXA-7)
    /// </summary>
    [DataElement(7, "Administered Units", ElementUsage.RequiredButMayBeEmpty)]
    public CE AdministeredUnits { get; set; } = new();

    /// <summary>
    /// Gets or sets the Administration Notes (RXA-9)
    /// </summary>
    [DataElement(9, "Administration Notes", ElementUsage.RequiredButMayBeEmpty)]
    public string AdministrationNotes { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Administering Provider (RXA-10)
    /// </summary>
    [DataElement(10, "Administering Provider", ElementUsage.RequiredButMayBeEmpty)]
    public XPN AdministeringProvider { get; set; } = new();

    /// <summary>
    /// Gets or sets the Administered at Location (RXA-11)
    /// </summary>
    [DataElement(11, "Administered at Location", ElementUsage.RequiredButMayBeEmpty)]
    public string AdministeredAtLocation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Administered Per (Time Unit) (RXA-12)
    /// </summary>
    [DataElement(12, "Administered Per (Time Unit)", ElementUsage.RequiredButMayBeEmpty)]
    public string AdministeredPer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Administered Strength (RXA-13)
    /// </summary>
    [DataElement(13, "Administered Strength", ElementUsage.RequiredButMayBeEmpty)]
    public string AdministeredStrength { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Administered Strength Units (RXA-14)
    /// </summary>
    [DataElement(14, "Administered Strength Units", ElementUsage.RequiredButMayBeEmpty)]
    public string AdministeredStrengthUnits { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Substance Lot Number (RXA-15)
    /// </summary>
    [DataElement(15, "Substance Lot Number", ElementUsage.RequiredButMayBeEmpty)]
    public string SubstanceLotNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Substance Expiration Date (RXA-16)
    /// </summary>
    [DataElement(16, "Substance Expiration Date", ElementUsage.RequiredButMayBeEmpty)]
    public string SubstanceExpirationDate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Substance Manufacturer Name (RXA-17)
    /// </summary>
    [DataElement(17, "Substance Manufacturer Name", ElementUsage.RequiredButMayBeEmpty)]
    public CE SubstanceManufacturerName { get; set; } = new();

    /// <summary>
    /// Gets or sets the Substance Refusal Reason (RXA-18)
    /// </summary>
    [DataElement(18, "Substance Refusal Reason", ElementUsage.Optional)]
    public CE SubstanceRefusalReason { get; set; } = new();

    /// <summary>
    /// Gets or sets the Indication (RXA-19)
    /// </summary>
    [DataElement(19, "Indication", ElementUsage.RequiredButMayBeEmpty)]
    public string Indication { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Completion Status (RXA-20)
    /// </summary>
    [DataElement(20, "Completion Status", ElementUsage.RequiredButMayBeEmpty)]
    public string CompletionStatus { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Action Code (RXA-21)
    /// </summary>
    [DataElement(21, "Action Code", ElementUsage.RequiredButMayBeEmpty)]
    public string ActionCode { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="RXASegment"/> class
    /// </summary>
    public RXASegment() : base("RXA")
    {
    }
}
