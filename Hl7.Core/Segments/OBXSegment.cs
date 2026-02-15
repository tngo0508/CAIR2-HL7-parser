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
    [DataElement(1, "Set ID - OBX", ElementUsage.Required)]
    public int SetId { get; set; }

    [DataElement(2, "Value Type", ElementUsage.Required)]
    public string ValueType { get; set; } = string.Empty;

    [DataElement(3, "Observation Identifier", ElementUsage.Required)]
    public string ObservationIdentifier { get; set; } = string.Empty;

    [DataElement(4, "Observation Sub-ID", ElementUsage.RequiredButMayBeEmpty)]
    public string ObservationSubId { get; set; } = string.Empty;

    [DataElement(5, "Observation Value", ElementUsage.RequiredButMayBeEmpty)]
    public string ObservationValue { get; set; } = string.Empty;

    [DataElement(6, "Units", ElementUsage.NotSupported)]
    public string Units { get; set; } = string.Empty;

    [DataElement(7, "Reference Range", ElementUsage.NotSupported)]
    public string ReferenceRange { get; set; } = string.Empty;

    [DataElement(8, "Abnormal Flags", ElementUsage.NotSupported)]
    public string AbnormalFlags { get; set; } = string.Empty;

    [DataElement(9, "Probability", ElementUsage.NotSupported)]
    public string Probability { get; set; } = string.Empty;

    [DataElement(10, "Nature of Abnormal Test", ElementUsage.NotSupported)]
    public string NatureOfAbnormalTest { get; set; } = string.Empty;

    [DataElement(11, "Observation Result Status", ElementUsage.Required)]
    public string ObservationResultStatus { get; set; } = string.Empty;

    [DataElement(12, "Date/Time of Observation", ElementUsage.Optional)]
    public string DateTimeOfObservation { get; set; } = string.Empty;

    [DataElement(13, "Producer's Reference", ElementUsage.Optional)]
    public string ProducersReference { get; set; } = string.Empty;

    [DataElement(14, "Responsible Observer", ElementUsage.Optional)]
    public string ResponsibleObserver { get; set; } = string.Empty;

    [DataElement(15, "Observation Method", ElementUsage.Optional)]
    public string ObservationMethod { get; set; } = string.Empty;

    public OBXSegment() : base("OBX")
    {
    }
}
