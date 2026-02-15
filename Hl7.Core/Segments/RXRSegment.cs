using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;

namespace Hl7.Core.Segments;

[Segment("RXR")]
public class RXRSegment : Segment
{
    [DataElement(1, "Route", ElementUsage.RequiredButMayBeEmpty)]
    public string Route { get; set; } = string.Empty;

    [DataElement(2, "Administration Site", ElementUsage.RequiredButMayBeEmpty)]
    public string AdministrationSite { get; set; } = string.Empty;

    public RXRSegment() : base("RXR")
    {
    }
}
