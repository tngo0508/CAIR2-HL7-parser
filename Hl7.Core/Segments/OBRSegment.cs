using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;

namespace Hl7.Core.Segments;

/// <summary>
/// OBR - Observation Request Segment
/// Used for vaccine orders and immunization requests
/// </summary>
[Segment("OBR")]
public class OBRSegment : Segment
{
    /// <summary>
    /// Gets or sets the Set ID - OBR (OBR-1)
    /// </summary>
    [DataElement(1, "Set ID - OBR", ElementUsage.Optional)]
    public int SetId { get; set; }

    /// <summary>
    /// Gets or sets the Placer Order Number (OBR-2)
    /// </summary>
    [DataElement(2, "Placer Order Number", ElementUsage.Optional)]
    public string PlacerOrderNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Filler Order Number (OBR-3)
    /// </summary>
    [DataElement(3, "Filler Order Number", ElementUsage.Optional)]
    public string FillerOrderNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Universal Service ID (OBR-4)
    /// </summary>
    [DataElement(4, "Universal Service ID", ElementUsage.Optional)]
    public string UniversalServiceId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Priority (OBR-5)
    /// </summary>
    [DataElement(5, "Priority", ElementUsage.Optional)]
    public int Priority { get; set; }

    /// <summary>
    /// Gets or sets the Requested Date/Time (OBR-6)
    /// </summary>
    [DataElement(6, "Requested Date/Time", ElementUsage.Optional)]
    public string RequestedDateTime { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Observation Date/Time (OBR-7)
    /// </summary>
    [DataElement(7, "Observation Date/Time", ElementUsage.Optional)]
    public string ObservationDateTime { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Observation End Date/Time (OBR-8)
    /// </summary>
    [DataElement(8, "Observation End Date/Time", ElementUsage.Optional)]
    public string ObservationEndDateTime { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Collector's Comment (OBR-9)
    /// </summary>
    [DataElement(9, "Collector's Comment", ElementUsage.Optional)]
    public string CollectorsComment { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Orderer's Comments (OBR-10)
    /// </summary>
    [DataElement(10, "Orderer's Comments", ElementUsage.Optional)]
    public string OrdererSComments { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Orderer's Name (OBR-11)
    /// </summary>
    [DataElement(11, "Orderer's Name", ElementUsage.Optional)]
    public string OrdererSName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Orderer's Address (OBR-12)
    /// </summary>
    [DataElement(12, "Orderer's Address", ElementUsage.Optional)]
    public string OrdererSAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Orderer's Phone Number (OBR-13)
    /// </summary>
    [DataElement(13, "Orderer's Phone Number", ElementUsage.Optional)]
    public string OrdererSPhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Orderer's Email Address (OBR-14)
    /// </summary>
    [DataElement(14, "Orderer's Email Address", ElementUsage.Optional)]
    public string OrdererSEmailAddress { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="OBRSegment"/> class
    /// </summary>
    public OBRSegment() : base("OBR")
    {
    }
}
