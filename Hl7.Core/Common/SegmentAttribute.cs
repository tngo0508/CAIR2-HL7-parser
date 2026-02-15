namespace Hl7.Core.Utils;

public class SegmentAttribute : Attribute
{
    public string SegmentId { get; set; }

    public SegmentAttribute(string segmentId)
    {
        SegmentId = segmentId;
    }
}