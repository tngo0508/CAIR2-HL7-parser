namespace Hl7.Core.Base;

/// <summary>
/// Represents an HL7 message containing multiple segments
/// </summary>
public class Hl7Message
{
    /// <summary>
    /// Gets or sets the list of segments in the message
    /// </summary>
    public List<Segment> Segments { get; set; } = [];

    /// <summary>
    /// Gets or sets the HL7 version of the message
    /// </summary>
    public string MessageVersion { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the separators used in the message
    /// </summary>
    public Hl7.Core.Common.Hl7Separators Separators { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Hl7Message"/> class
    /// </summary>
    public Hl7Message() { }

    /// <summary>
    /// Gets the first segment of a specific type
    /// </summary>
    /// <typeparam name="T">The type of segment</typeparam>
    /// <param name="segmentId">The segment identifier</param>
    /// <returns>The segment if found; otherwise, null</returns>
    public T? GetSegment<T>(string segmentId) where T : Segment
    {
        return Segments.FirstOrDefault(s => s.SegmentId == segmentId) as T;
    }

    /// <summary>
    /// Gets all segments of a specific type
    /// </summary>
    /// <typeparam name="T">The type of segment</typeparam>
    /// <param name="segmentId">The segment identifier</param>
    /// <returns>A list of segments</returns>
    public List<T> GetSegments<T>(string segmentId) where T : Segment
    {
        return Segments.Where(s => s.SegmentId == segmentId).Cast<T>().ToList();
    }

    /// <summary>
    /// Adds a segment to the message
    /// </summary>
    /// <param name="segment">The segment to add</param>
    public void AddSegment(Segment segment)
    {
        Segments.Add(segment);
    }

    /// <summary>
    /// Removes a segment from the message
    /// </summary>
    /// <param name="segment">The segment to remove</param>
    public void RemoveSegment(Segment segment)
    {
        Segments.Remove(segment);
    }
}
