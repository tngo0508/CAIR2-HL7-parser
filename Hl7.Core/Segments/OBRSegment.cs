namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

/// <summary>
/// OBR - Observation Request Segment
/// Used for vaccine orders and immunization requests
/// </summary>
[Segment("OBR")]
public class OBRSegment : Segment
{
    [DataElement(1)]
    public int SetId { get; set; }

    [DataElement(2)]
    public string PlacerOrderNumber { get; set; } = string.Empty;

    [DataElement(3)]
    public string FillerOrderNumber { get; set; } = string.Empty;

    [DataElement(4)]
    public string UniversalServiceId { get; set; } = string.Empty;

    [DataElement(5)]
    public int Priority { get; set; }

    [DataElement(6)]
    public string RequestedDateTime { get; set; } = string.Empty;

    [DataElement(7)]
    public string ObservationDateTime { get; set; } = string.Empty;

    [DataElement(8)]
    public string ObservationEndDateTime { get; set; } = string.Empty;

    [DataElement(9)]
    public string CollectorsComment { get; set; } = string.Empty;

    [DataElement(10)]
    public string OrdererSComments { get; set; } = string.Empty;

    [DataElement(11)]
    public string OrdererSName { get; set; } = string.Empty;

    [DataElement(12)]
    public string OrdererSAddress { get; set; } = string.Empty;

    [DataElement(13)]
    public string OrdererSPhoneNumber { get; set; } = string.Empty;

    [DataElement(14)]
    public string OrdererSEmailAddress { get; set; } = string.Empty;

    public OBRSegment() : base("OBR")
    {
    }
}
