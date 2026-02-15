using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Utils;

namespace Hl7.Core.Segments;

/// <summary>
/// MSH - Message Header Segment
/// </summary>
[Segment("MSH")]
public class MSHSegment : Segment
{
    /// <summary>
    /// Gets or sets the encoding characters (MSH-2)
    /// </summary>
    [DataElement(2, "Encoding Characters", ElementUsage.Required)]
    public string EncodingCharacters { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sending application (MSH-3)
    /// </summary>
    [DataElement(3, "Sending Application", ElementUsage.Required)]
    public string SendingApplication { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sending facility (MSH-4)
    /// </summary>
    [DataElement(4, "Sending Facility", ElementUsage.Required)]
    public string SendingFacility { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the receiving application (MSH-5)
    /// </summary>
    [DataElement(5, "Receiving Application", ElementUsage.Required)]
    public string ReceivingApplication { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the receiving facility (MSH-6)
    /// </summary>
    [DataElement(6, "Receiving Facility", ElementUsage.Required)]
    public string ReceivingFacility { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date/time of the message (MSH-7)
    /// </summary>
    [DataElement(7, "Date/Time of Message", ElementUsage.Required)]
    public string MessageDateTime { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the security field (MSH-8)
    /// </summary>
    [DataElement(8, "Security", ElementUsage.Optional)]
    public string Security { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message type (MSH-9)
    /// </summary>
    [DataElement(9, "Message Type", ElementUsage.Required)]
    public string MessageType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message control ID (MSH-10)
    /// </summary>
    [DataElement(10, "Message Control ID", ElementUsage.Required)]
    public string MessageControlId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the processing ID (MSH-11)
    /// </summary>
    [DataElement(11, "Processing ID", ElementUsage.Required)]
    public string ProcessingId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the version ID (MSH-12)
    /// </summary>
    [DataElement(12, "Version ID", ElementUsage.Required)]
    public string VersionId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sequence number (MSH-13)
    /// </summary>
    [DataElement(13, "Sequence Number", ElementUsage.Optional)]
    public string SequenceNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the continuation pointer (MSH-14)
    /// </summary>
    [DataElement(14, "Continuation Pointer", ElementUsage.Optional)]
    public string ContinuationPointer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the accept acknowledgment type (MSH-15)
    /// </summary>
    [DataElement(15, "Accept Acknowledgment Type", ElementUsage.Required)]
    public string AcceptAcknowledgmentType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the application acknowledgment type (MSH-16)
    /// </summary>
    [DataElement(16, "Application Acknowledgment Type", ElementUsage.Required)]
    public string ApplicationAcknowledgmentType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the country code (MSH-17)
    /// </summary>
    [DataElement(17, "Country Code", ElementUsage.Optional)]
    public string CountryCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the character set (MSH-18)
    /// </summary>
    [DataElement(18, "Character Set", ElementUsage.Optional)]
    public string CharacterSet { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the principal language of the message (MSH-19)
    /// </summary>
    [DataElement(19, "Principal Language of Message", ElementUsage.Optional)]
    public string PrincipalLanguageOfMessage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the alternate character set handling scheme (MSH-20)
    /// </summary>
    [DataElement(20, "Alternate Character Set Handling Scheme", ElementUsage.Optional)]
    public string AlternateCharacterSetHandlingScheme { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message profile identifier (MSH-21)
    /// </summary>
    [DataElement(21, "Message Profile Identifier", ElementUsage.Required)]
    public string MessageProfileIdentifier { get; set; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="MSHSegment"/> class
    /// </summary>
    public MSHSegment() : base("MSH")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MSHSegment"/> class with sending and receiving applications
    /// </summary>
    /// <param name="sendingApplication">The sending application</param>
    /// <param name="receivingApplication">The receiving application</param>
    public MSHSegment(string sendingApplication, string receivingApplication) : base("MSH")
    {
        SendingApplication = sendingApplication;
        ReceivingApplication = receivingApplication;
    }
}
