namespace Hl7.Core.Common;

/// <summary>
/// Attribute used to mark a class as an HL7 segment
/// </summary>
public class SegmentAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the segment identifier (e.g., "MSH", "PID")
    /// </summary>
    public string SegmentId { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SegmentAttribute"/> class
    /// </summary>
    /// <param name="segmentId">The segment identifier</param>
    public SegmentAttribute(string segmentId)
    {
        SegmentId = segmentId;
    }
}