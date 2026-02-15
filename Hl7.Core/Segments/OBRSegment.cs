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
    [DataElement(1, "Set ID - OBR", ElementUsage.Optional)]
    public int SetId { get; set; }

    [DataElement(2, "Placer Order Number", ElementUsage.Optional)]
    public string PlacerOrderNumber { get; set; } = string.Empty;

    [DataElement(3, "Filler Order Number", ElementUsage.Optional)]
    public string FillerOrderNumber { get; set; } = string.Empty;

    [DataElement(4, "Universal Service ID", ElementUsage.Optional)]
    public string UniversalServiceId { get; set; } = string.Empty;

    [DataElement(5, "Priority", ElementUsage.Optional)]
    public int Priority { get; set; }

    [DataElement(6, "Requested Date/Time", ElementUsage.Optional)]
    public string RequestedDateTime { get; set; } = string.Empty;

    [DataElement(7, "Observation Date/Time", ElementUsage.Optional)]
    public string ObservationDateTime { get; set; } = string.Empty;

    [DataElement(8, "Observation End Date/Time", ElementUsage.Optional)]
    public string ObservationEndDateTime { get; set; } = string.Empty;

    [DataElement(9, "Collector's Comment", ElementUsage.Optional)]
    public string CollectorsComment { get; set; } = string.Empty;

    [DataElement(10, "Orderer's Comments", ElementUsage.Optional)]
    public string OrdererSComments { get; set; } = string.Empty;

    [DataElement(11, "Orderer's Name", ElementUsage.Optional)]
    public string OrdererSName { get; set; } = string.Empty;

    [DataElement(12, "Orderer's Address", ElementUsage.Optional)]
    public string OrdererSAddress { get; set; } = string.Empty;

    [DataElement(13, "Orderer's Phone Number", ElementUsage.Optional)]
    public string OrdererSPhoneNumber { get; set; } = string.Empty;

    [DataElement(14, "Orderer's Email Address", ElementUsage.Optional)]
    public string OrdererSEmailAddress { get; set; } = string.Empty;

    public OBRSegment() : base("OBR")
    {
    }
}
