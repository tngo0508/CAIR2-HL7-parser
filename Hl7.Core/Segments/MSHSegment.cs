namespace Hl7.Core.Segments;

using Hl7.Core.Base;
using Hl7.Core.Utils;

[Segment("MSH")]
public class MSHSegment : Segment
{
    [DataElement(2)]
    public string EncodingCharacters { get; set; } = string.Empty;

    [DataElement(3)]
    public string SendingApplication { get; set; } = string.Empty;

    [DataElement(4)]
    public string SendingFacility { get; set; } = string.Empty;

    [DataElement(5)]
    public string ReceivingApplication { get; set; } = string.Empty;

    [DataElement(6)]
    public string ReceivingFacility { get; set; } = string.Empty;

    [DataElement(7)]
    public string MessageDateTime { get; set; } = string.Empty;

    [DataElement(8)]
    public string Security { get; set; } = string.Empty;

    [DataElement(9)]
    public string MessageType { get; set; } = string.Empty;

    [DataElement(10)]
    public string MessageControlId { get; set; } = string.Empty;

    [DataElement(11)]
    public string ProcessingId { get; set; } = string.Empty;

    [DataElement(12)]
    public string VersionId { get; set; } = string.Empty;

    [DataElement(13)]
    public string SequenceNumber { get; set; } = string.Empty;

    [DataElement(14)]
    public string ContinuationPointer { get; set; } = string.Empty;

    [DataElement(15)]
    public string AcceptAcknowledgmentType { get; set; } = string.Empty;

    [DataElement(16)]
    public string ApplicationAcknowledgmentType { get; set; } = string.Empty;

    [DataElement(17)]
    public string CountryCode { get; set; } = string.Empty;

    [DataElement(18)]
    public string CharacterSet { get; set; } = string.Empty;

    [DataElement(19)]
    public string PrincipalLanguageOfMessage { get; set; } = string.Empty;

    [DataElement(20)]
    public string AlternateCharacterSetHandlingScheme { get; set; } = string.Empty;

    [DataElement(21)]
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
