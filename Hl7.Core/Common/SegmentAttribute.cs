namespace Hl7.Core.Common;

public class SegmentAttribute : Attribute
{
    public string SegmentId { get; set; }

    public SegmentAttribute(string segmentId)
    {
        SegmentId = segmentId;
    }
}