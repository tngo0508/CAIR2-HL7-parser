using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;

namespace Hl7.Core.Segments;

/// <summary>
/// MSA - Message Acknowledgment Segment
/// </summary>
[Segment("MSA")]
public class MSASegment : Segment
{
    /// <summary>
    /// Gets or sets the acknowledgment code (MSA-1)
    /// </summary>
    [DataElement(1, "Acknowledgment Code", ElementUsage.Required)]
    public string AcknowledgmentCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message control ID (MSA-2)
    /// </summary>
    [DataElement(2, "Message Control ID", ElementUsage.Required)]
    public string MessageControlId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the text message (MSA-3)
    /// </summary>
    [DataElement(3, "Text Message", ElementUsage.RequiredButMayBeEmpty)]
    public string TextMessage { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="MSASegment"/> class
    /// </summary>
    public MSASegment() : base("MSA")
    {
    }
}
