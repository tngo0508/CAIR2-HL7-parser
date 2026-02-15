namespace Hl7.Core.Base;

/// <summary>
/// Represents an HL7 message containing multiple segments
/// </summary>
public class Hl7Message
{
    public List<Segment> Segments { get; set; } = [];
    public string MessageVersion { get; set; } = string.Empty;

    public Hl7Message() { }

    public T? GetSegment<T>(string segmentId) where T : Segment
    {
        return Segments.FirstOrDefault(s => s.SegmentId == segmentId) as T;
    }

    public List<T> GetSegments<T>(string segmentId) where T : Segment
    {
        return Segments.Where(s => s.SegmentId == segmentId).Cast<T>().ToList();
    }

    public void AddSegment(Segment segment)
    {
        Segments.Add(segment);
    }

    public void RemoveSegment(Segment segment)
    {
        Segments.Remove(segment);
    }
}
