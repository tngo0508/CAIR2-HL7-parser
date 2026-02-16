using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;

namespace Hl7.Core.Segments;

/// <summary>
/// ERR - Error Segment
/// </summary>
[Segment("ERR")]
public class ERRSegment : Segment
{
    /// <summary>
    /// Gets or sets the error code and location (ERR-1)
    /// </summary>
    [DataElement(1, "Error Code and Location", ElementUsage.Optional)]
    public string ErrorCodeAndLocation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the error location (ERR-2)
    /// </summary>
    [DataElement(2, "Error Location", ElementUsage.Optional)]
    public string ErrorLocation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the HL7 error code (ERR-3)
    /// </summary>
    [DataElement(3, "HL7 Error Code", ElementUsage.Optional)]
    public string Hl7ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the severity (ERR-4)
    /// </summary>
    [DataElement(4, "Severity", ElementUsage.Optional)]
    public string Severity { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="ERRSegment"/> class
    /// </summary>
    public ERRSegment() : base("ERR")
    {
    }
}
