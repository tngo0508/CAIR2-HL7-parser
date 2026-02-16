using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;
using Hl7.Core.Types;

namespace Hl7.Core.Segments;

/// <summary>
/// OBX - Observation/Result Segment
/// Used for vaccine details and immunization results
/// </summary>
[Segment("OBX")]
public class OBXSegment : Segment
{
    /// <summary>
    /// Gets or sets the Set ID - OBX (OBX-1)
    /// </summary>
    [DataElement(1, "Set ID - OBX", ElementUsage.Required)]
    public int SetId { get; set; }

    /// <summary>
    /// Gets or sets the Value Type (OBX-2)
    /// </summary>
    [DataElement(2, "Value Type", ElementUsage.Required)]
    public string ValueType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Observation Identifier (OBX-3)
    /// </summary>
    [DataElement(3, "Observation Identifier", ElementUsage.Required)]
    public CE ObservationIdentifier { get; set; } = new();

    /// <summary>
    /// Gets or sets the Observation Sub-ID (OBX-4)
    /// </summary>
    [DataElement(4, "Observation Sub-ID", ElementUsage.RequiredButMayBeEmpty)]
    public string ObservationSubId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Observation Value (OBX-5)
    /// </summary>
    [DataElement(5, "Observation Value", ElementUsage.RequiredButMayBeEmpty)]
    public string ObservationValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Units (OBX-6)
    /// </summary>
    [DataElement(6, "Units", ElementUsage.NotSupported)]
    public string Units { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Reference Range (OBX-7)
    /// </summary>
    [DataElement(7, "Reference Range", ElementUsage.NotSupported)]
    public string ReferenceRange { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Abnormal Flags (OBX-8)
    /// </summary>
    [DataElement(8, "Abnormal Flags", ElementUsage.NotSupported)]
    public string AbnormalFlags { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Probability (OBX-9)
    /// </summary>
    [DataElement(9, "Probability", ElementUsage.NotSupported)]
    public string Probability { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Nature of Abnormal Test (OBX-10)
    /// </summary>
    [DataElement(10, "Nature of Abnormal Test", ElementUsage.NotSupported)]
    public string NatureOfAbnormalTest { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Observation Result Status (OBX-11)
    /// </summary>
    [DataElement(11, "Observation Result Status", ElementUsage.Required)]
    public string ObservationResultStatus { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Date/Time of Observation (OBX-12)
    /// </summary>
    [DataElement(12, "Date/Time of Observation", ElementUsage.Optional)]
    public string DateTimeOfObservation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Producer's Reference (OBX-13)
    /// </summary>
    [DataElement(13, "Producer's Reference", ElementUsage.Optional)]
    public string ProducersReference { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Responsible Observer (OBX-14)
    /// </summary>
    [DataElement(14, "Responsible Observer", ElementUsage.Optional)]
    public string ResponsibleObserver { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Observation Method (OBX-15)
    /// </summary>
    [DataElement(15, "Observation Method", ElementUsage.Optional)]
    public string ObservationMethod { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Responsible Observer (OBX-16)
    /// </summary>
    [DataElement(16, "Responsible Observer", ElementUsage.Optional)]
    public XPN ResponsibleObserverXpn { get; set; } = new();

    /// <summary>
    /// Gets or sets the Observation Method (OBX-17)
    /// </summary>
    [DataElement(17, "Observation Method (CE)", ElementUsage.Optional)]
    public CE ObservationMethodCe { get; set; } = new();

    /// <summary>
    /// Gets or sets the Equipment Instance Identifier (OBX-18)
    /// </summary>
    [DataElement(18, "Equipment Instance Identifier", ElementUsage.Optional)]
    public string EquipmentInstanceIdentifier { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Date/Time of the Analysis (OBX-19)
    /// </summary>
    [DataElement(19, "Date/Time of the Analysis", ElementUsage.Optional)]
    public string DateTimeOfTheAnalysis { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="OBXSegment"/> class
    /// </summary>
    public OBXSegment() : base("OBX")
    {
    }
}
