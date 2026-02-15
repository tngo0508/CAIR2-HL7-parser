using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;

namespace Hl7.Core.Segments;

[Segment("MSH")]
public class MSHSegment : Segment
{
    [DataElement(2, "Encoding Characters", ElementUsage.Required)]
    public string EncodingCharacters { get; set; } = string.Empty;

    [DataElement(3, "Sending Application", ElementUsage.Required)]
    public string SendingApplication { get; set; } = string.Empty;

    [DataElement(4, "Sending Facility", ElementUsage.Required)]
    public string SendingFacility { get; set; } = string.Empty;

    [DataElement(5, "Receiving Application", ElementUsage.Required)]
    public string ReceivingApplication { get; set; } = string.Empty;

    [DataElement(6, "Receiving Facility", ElementUsage.Required)]
    public string ReceivingFacility { get; set; } = string.Empty;

    [DataElement(7, "Date/Time of Message", ElementUsage.Required)]
    public string MessageDateTime { get; set; } = string.Empty;

    [DataElement(8, "Security", ElementUsage.Optional)]
    public string Security { get; set; } = string.Empty;

    [DataElement(9, "Message Type", ElementUsage.Required)]
    public string MessageType { get; set; } = string.Empty;

    [DataElement(10, "Message Control ID", ElementUsage.Required)]
    public string MessageControlId { get; set; } = string.Empty;

    [DataElement(11, "Processing ID", ElementUsage.Required)]
    public string ProcessingId { get; set; } = string.Empty;

    [DataElement(12, "Version ID", ElementUsage.Required)]
    public string VersionId { get; set; } = string.Empty;

    [DataElement(13, "Sequence Number", ElementUsage.Optional)]
    public string SequenceNumber { get; set; } = string.Empty;

    [DataElement(14, "Continuation Pointer", ElementUsage.Optional)]
    public string ContinuationPointer { get; set; } = string.Empty;

    [DataElement(15, "Accept Acknowledgment Type", ElementUsage.Required)]
    public string AcceptAcknowledgmentType { get; set; } = string.Empty;

    [DataElement(16, "Application Acknowledgment Type", ElementUsage.Required)]
    public string ApplicationAcknowledgmentType { get; set; } = string.Empty;

    [DataElement(17, "Country Code", ElementUsage.Optional)]
    public string CountryCode { get; set; } = string.Empty;

    [DataElement(18, "Character Set", ElementUsage.Optional)]
    public string CharacterSet { get; set; } = string.Empty;

    [DataElement(19, "Principal Language of Message", ElementUsage.Optional)]
    public string PrincipalLanguageOfMessage { get; set; } = string.Empty;

    [DataElement(20, "Alternate Character Set Handling Scheme", ElementUsage.Optional)]
    public string AlternateCharacterSetHandlingScheme { get; set; } = string.Empty;

    [DataElement(21, "Message Profile Identifier", ElementUsage.Required)]
    public string MessageProfileIdentifier { get; set; } = string.Empty;

    public MSHSegment() : base("MSH")
    {
    }

    public MSHSegment(string sendingApplication, string receivingApplication) : base("MSH")
    {
        SendingApplication = sendingApplication;
        ReceivingApplication = receivingApplication;
    }
}
