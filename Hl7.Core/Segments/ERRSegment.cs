using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;
using Hl7.Core.Types;

namespace Hl7.Core.Segments;

/// <summary>
/// ERR - Error Segment
/// </summary>
[Segment("ERR")]
public class ERRSegment : Segment
{
    /// <summary>
    /// Gets or sets the error location (ERR-2)
    /// </summary>
    [DataElement(2, "Error Location", ElementUsage.RequiredButMayBeEmpty)]
    public ERN ErrorLocation { get; set; } = new();

    /// <summary>
    /// Gets or sets the HL7 error code (ERR-3)
    /// </summary>
    [DataElement(3, "HL7 Error Code", ElementUsage.Required)]
    public CE Hl7ErrorCode { get; set; } = new();

    /// <summary>
    /// Gets or sets the severity (ERR-4)
    /// </summary>
    [DataElement(4, "Severity", ElementUsage.Required)]
    public string Severity { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Application Error Code (ERR-5)
    /// </summary>
    [DataElement(5, "Application Error Code", ElementUsage.Optional)]
    public CE ApplicationErrorCode { get; set; } = new();

    /// <summary>
    /// Gets or sets the User Message (ERR-8)
    /// </summary>
    [DataElement(8, "User Message", ElementUsage.Optional)]
    public string UserMessage { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="ERRSegment"/> class
    /// </summary>
    public ERRSegment() : base("ERR")
    {
    }
}
