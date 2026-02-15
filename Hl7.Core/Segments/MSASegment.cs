using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;

namespace Hl7.Core.Segments;

[Segment("MSA")]
public class MSASegment : Segment
{
    [DataElement(1, "Acknowledgment Code", ElementUsage.Required)]
    public string AcknowledgmentCode { get; set; } = string.Empty;

    [DataElement(2, "Message Control ID", ElementUsage.Required)]
    public string MessageControlId { get; set; } = string.Empty;

    [DataElement(3, "Text Message", ElementUsage.RequiredButMayBeEmpty)]
    public string TextMessage { get; set; } = string.Empty;

    public MSASegment() : base("MSA")
    {
    }
}
