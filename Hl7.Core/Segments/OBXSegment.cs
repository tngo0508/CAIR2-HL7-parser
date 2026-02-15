namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

/// <summary>
/// OBX - Observation/Result Segment
/// Used for vaccine details and immunization results
/// </summary>
[Segment("OBX")]
public class OBXSegment : Segment
{
    [DataElement(1)]
    public int SetId { get; set; }

    [DataElement(2)]
    public string ValueType { get; set; } = string.Empty;

    [DataElement(3)]
    public string ObservationIdentifier { get; set; } = string.Empty;

    [DataElement(4)]
    public string ObservationSubId { get; set; } = string.Empty;

    [DataElement(5)]
    public string ObservationValue { get; set; } = string.Empty;

    [DataElement(6)]
    public string Units { get; set; } = string.Empty;

    [DataElement(7)]
    public string ReferenceRange { get; set; } = string.Empty;

    [DataElement(8)]
    public string AbnormalFlags { get; set; } = string.Empty;

    [DataElement(9)]
    public string Probability { get; set; } = string.Empty;

    [DataElement(10)]
    public string NatureOfAbnormalTest { get; set; } = string.Empty;

    [DataElement(11)]
    public string ObservationResultStatus { get; set; } = string.Empty;

    [DataElement(12)]
    public string DateTimeOfObservation { get; set; } = string.Empty;

    [DataElement(13)]
    public string ProducersReference { get; set; } = string.Empty;

    [DataElement(14)]
    public string ResponsibleObserver { get; set; } = string.Empty;

    [DataElement(15)]
    public string ObservationMethod { get; set; } = string.Empty;

    public OBXSegment() : base("OBX")
    {
    }
}
