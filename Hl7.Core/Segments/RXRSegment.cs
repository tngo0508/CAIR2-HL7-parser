using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;

namespace Hl7.Core.Segments;

/// <summary>
/// RXR - Pharmacy/Treatment Route Segment
/// </summary>
[Segment("RXR")]
public class RXRSegment : Segment
{
    /// <summary>
    /// Gets or sets the route (RXR-1)
    /// </summary>
    [DataElement(1, "Route", ElementUsage.RequiredButMayBeEmpty)]
    public string Route { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the administration site (RXR-2)
    /// </summary>
    [DataElement(2, "Administration Site", ElementUsage.RequiredButMayBeEmpty)]
    public string AdministrationSite { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="RXRSegment"/> class
    /// </summary>
    public RXRSegment() : base("RXR")
    {
    }
}
