namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("ORC")]
public class ORCSegment : Segment
{
    [DataElement(1)]
    public string OrderControl { get; set; } = string.Empty;

    [DataElement(2)]
    public string PlacerOrderNumber { get; set; } = string.Empty;

    [DataElement(3)]
    public string FillerOrderNumber { get; set; } = string.Empty;

    [DataElement(10)]
    public string EnteredBy { get; set; } = string.Empty;

    [DataElement(12)]
    public string OrderingProvider { get; set; } = string.Empty;

    [DataElement(17)]
    public string EnteringOrganization { get; set; } = string.Empty;

    public ORCSegment() : base("ORC")
    {
    }
}
