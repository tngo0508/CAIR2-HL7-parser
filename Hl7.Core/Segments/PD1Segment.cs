using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;

namespace Hl7.Core.Segments;

/// <summary>
/// PD1 - Patient Additional Demographic Segment
/// </summary>
[Segment("PD1")]
public class PD1Segment : Segment
{
    /// <summary>
    /// Gets or sets the publicity code (PD1-11)
    /// </summary>
    [DataElement(11, "Publicity Code", ElementUsage.Optional)]
    public string PublicityCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the protection indicator (PD1-12)
    /// </summary>
    [DataElement(12, "Protection Indicator", ElementUsage.Optional)]
    public string ProtectionIndicator { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the protection indicator effective date (PD1-13)
    /// </summary>
    [DataElement(13, "Protection Indicator Effective Date", ElementUsage.Optional)]
    public string ProtectionIndicatorEffectiveDate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the immunization registry status (PD1-16)
    /// </summary>
    [DataElement(16, "Immunization Registry Status", ElementUsage.Optional)]
    public string ImmunizationRegistryStatus { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the immunization registry status effective date (PD1-17)
    /// </summary>
    [DataElement(17, "Immunization Registry Status Effective Date", ElementUsage.Optional)]
    public string ImmunizationRegistryStatusEffectiveDate { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="PD1Segment"/> class
    /// </summary>
    public PD1Segment() : base("PD1")
    {
    }
}
