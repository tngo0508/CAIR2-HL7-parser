namespace Hl7.Core.Base;

/// <summary>
/// Represents a generic HL7 segment
/// </summary>
public class Segment
{
    /// <summary>
    /// Gets or sets the segment identifier (e.g., MSH, PID)
    /// </summary>
    public string SegmentId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the dictionary of fields in the segment
    /// </summary>
    public Dictionary<int, string> Fields { get; set; } = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="Segment"/> class
    /// </summary>
    public Segment() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Segment"/> class with a segment identifier
    /// </summary>
    /// <param name="segmentId">The segment identifier</param>
    public Segment(string segmentId)
    {
        SegmentId = segmentId;
    }
}