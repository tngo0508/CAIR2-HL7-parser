namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("RXR")]
public class RXRSegment : Segment
{
    [DataElement(1)]
    public string Route { get; set; } = string.Empty;

    [DataElement(2)]
    public string AdministrationSite { get; set; } = string.Empty;

    public RXRSegment() : base("RXR")
    {
    }
}
