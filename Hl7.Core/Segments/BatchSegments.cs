using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Types;

namespace Hl7.Core.Segments;

/// <summary>
/// FHS - File Header Segment
/// </summary>
[Segment("FHS")]
public class FHSSegment : Segment
{
    [DataElement(1, "File Field Separator", ElementUsage.Required)]
    public string FileFieldSeparator { get; set; } = string.Empty;

    [DataElement(2, "File Encoding Characters", ElementUsage.Required)]
    public string FileEncodingCharacters { get; set; } = string.Empty;

    [DataElement(3, "File Sending Application", ElementUsage.Optional)]
    public HD FileSendingApplication { get; set; } = new();

    [DataElement(4, "File Sending Facility", ElementUsage.Optional)]
    public HD FileSendingFacility { get; set; } = new();

    [DataElement(5, "File Receiving Application", ElementUsage.Optional)]
    public HD FileReceivingApplication { get; set; } = new();

    [DataElement(6, "File Receiving Facility", ElementUsage.Optional)]
    public HD FileReceivingFacility { get; set; } = new();

    [DataElement(7, "File Creation Date/Time", ElementUsage.Optional)]
    public TSU FileCreationDateTime { get; set; } = new();

    public FHSSegment() : base("FHS") { }
}

/// <summary>
/// FTS - File Trailer Segment
/// </summary>
[Segment("FTS")]
public class FTSSegment : Segment
{
    [DataElement(1, "File Batch Count", ElementUsage.RequiredButMayBeEmpty)]
    public string FileBatchCount { get; set; } = string.Empty;

    [DataElement(2, "File Trailer Information", ElementUsage.Optional)]
    public string FileTrailerInformation { get; set; } = string.Empty;

    public FTSSegment() : base("FTS") { }
}

/// <summary>
/// BHS - Batch Header Segment
/// </summary>
[Segment("BHS")]
public class BHSSegment : Segment
{
    [DataElement(1, "Batch Field Separator", ElementUsage.Required)]
    public string BatchFieldSeparator { get; set; } = string.Empty;

    [DataElement(2, "Batch Encoding Characters", ElementUsage.Required)]
    public string BatchEncodingCharacters { get; set; } = string.Empty;

    [DataElement(3, "Batch Sending Application", ElementUsage.Optional)]
    public HD BatchSendingApplication { get; set; } = new();

    [DataElement(4, "Batch Sending Facility", ElementUsage.Optional)]
    public HD BatchSendingFacility { get; set; } = new();

    [DataElement(5, "Batch Receiving Application", ElementUsage.Optional)]
    public HD BatchReceivingApplication { get; set; } = new();

    [DataElement(6, "Batch Receiving Facility", ElementUsage.Optional)]
    public HD BatchReceivingFacility { get; set; } = new();

    [DataElement(7, "Batch Creation Date/Time", ElementUsage.Optional)]
    public TSU BatchCreationDateTime { get; set; } = new();

    public BHSSegment() : base("BHS") { }
}

/// <summary>
/// BTS - Batch Trailer Segment
/// </summary>
[Segment("BTS")]
public class BTSSegment : Segment
{
    [DataElement(1, "Batch Message Count", ElementUsage.RequiredButMayBeEmpty)]
    public string BatchMessageCount { get; set; } = string.Empty;

    [DataElement(2, "Batch Trailer Information", ElementUsage.Optional)]
    public string BatchTrailerInformation { get; set; } = string.Empty;

    public BTSSegment() : base("BTS") { }
}
