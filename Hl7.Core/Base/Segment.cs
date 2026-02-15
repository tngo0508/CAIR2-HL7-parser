namespace Hl7.Core.Base;

public class Segment
{
    public string SegmentId { get; set; } = string.Empty;
    public Dictionary<int, string> Fields { get; set; } = [];

    public Segment() { }

    public Segment(string segmentId)
    {
        SegmentId = segmentId;
    }
}