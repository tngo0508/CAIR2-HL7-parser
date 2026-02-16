namespace Hl7.Core.CAIR2;

using Hl7.Core.Base;
using Hl7.Core.Segments;

/// <summary>
/// Supported CAIR2 query profiles
/// </summary>
public enum Cair2QueryProfile
{
    /// <summary>
    /// Z34 - Request Immunization History
    /// </summary>
    Z34,

    /// <summary>
    /// Z44 - Request Immunization History and Forecast
    /// </summary>
    Z44
}

/// <summary>
/// Parameters for building a CAIR2 query (QBP) message
/// </summary>
public class Cair2QueryParameters
{
    /// <summary>
    /// Gets or sets the sending application
    /// </summary>
    public string SendingApplication { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sending facility
    /// </summary>
    public string SendingFacility { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the receiving application
    /// </summary>
    public string ReceivingApplication { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the receiving facility
    /// </summary>
    public string ReceivingFacility { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message control ID
    /// </summary>
    public string MessageControlId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the query tag
    /// </summary>
    public string QueryTag { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient identifier list
    /// </summary>
    public string PatientIdentifierList { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient name
    /// </summary>
    public string PatientName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the mother's maiden name
    /// </summary>
    public string MothersMaidenName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient's date of birth
    /// </summary>
    public string DateOfBirth { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient's sex
    /// </summary>
    public string AdministrativeSex { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient's address
    /// </summary>
    public string PatientAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient's home phone number
    /// </summary>
    public string PhoneNumberHome { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the multiple birth indicator
    /// </summary>
    public string MultipleBirthIndicator { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the birth order
    /// </summary>
    public string BirthOrder { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message date and time
    /// </summary>
    public DateTime? MessageDateTime { get; set; }

    /// <summary>
    /// Gets or sets the processing ID (default is "P")
    /// </summary>
    public string ProcessingId { get; set; } = "P";
}

/// <summary>
/// Represents the response to a CAIR2 query
/// </summary>
public class Cair2QueryResponse
{
    /// <summary>
    /// Gets or sets the query tag
    /// </summary>
    public string QueryTag { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the query response status
    /// </summary>
    public string QueryResponseStatus { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the acknowledgment code
    /// </summary>
    public string AcknowledgmentCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message profile identifier
    /// </summary>
    public string MessageProfileIdentifier { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message metadata
    /// </summary>
    public MessageMetadata? Metadata { get; set; }

    /// <summary>
    /// Gets or sets the patient info
    /// </summary>
    public PatientInfo? Patient { get; set; }

    /// <summary>
    /// Gets or sets the list of vaccination records
    /// </summary>
    public List<VaccinationRecord> VaccinationRecords { get; set; } = [];

    /// <summary>
    /// Gets or sets the raw HL7 message
    /// </summary>
    public Hl7Message? RawMessage { get; set; }
}

/// <summary>
/// Facilitates bidirectional exchange (QBP/RSP) with CAIR2
/// </summary>
public class Cair2BidirectionalExchange
{
    private readonly Hl7Parser _parser = new();
    private readonly CAIR2Parser _cair2Parser = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Cair2BidirectionalExchange"/> class
    /// </summary>
    public Cair2BidirectionalExchange() { }

    /// <summary>
    /// Builds a QBP (Query by Parameter) message for CAIR2
    /// </summary>
    /// <param name="profile">The query profile to use</param>
    /// <param name="parameters">The query parameters</param>
    /// <returns>An <see cref="Hl7Message"/> representing the query</returns>
    public Hl7Message BuildQbpMessage(Cair2QueryProfile profile, Cair2QueryParameters parameters)
    {
        if (parameters == null)
            throw new ArgumentNullException(nameof(parameters));

        var messageDateTime = parameters.MessageDateTime ?? DateTime.UtcNow;

        var msh = new MSHSegment
        {
            SendingApplication = parameters.SendingApplication,
            SendingFacility = parameters.SendingFacility,
            ReceivingApplication = parameters.ReceivingApplication,
            ReceivingFacility = parameters.ReceivingFacility,
            MessageDateTime = messageDateTime.ToString("yyyyMMddHHmmss"),
            MessageType = "QBP^Q11^QBP_Q11",
            MessageControlId = parameters.MessageControlId,
            ProcessingId = parameters.ProcessingId,
            VersionId = "2.5.1",
            AcceptAcknowledgmentType = "ER",
            ApplicationAcknowledgmentType = "AL",
            MessageProfileIdentifier = BuildProfileIdentifier(profile)
        };

        var qpd = new QPDSegment
        {
            MessageQueryName = BuildQueryName(profile),
            QueryTag = parameters.QueryTag,
            PatientIdentifierList = parameters.PatientIdentifierList,
            PatientName = parameters.PatientName,
            MothersMaidenName = parameters.MothersMaidenName,
            DateOfBirth = parameters.DateOfBirth,
            AdministrativeSex = parameters.AdministrativeSex,
            PatientAddress = parameters.PatientAddress,
            PhoneNumberHome = parameters.PhoneNumberHome,
            MultipleBirthIndicator = parameters.MultipleBirthIndicator,
            BirthOrder = parameters.BirthOrder
        };

        var rcp = new RCPSegment
        {
            QueryPriority = "I",
            QuantityLimitedRequest = "5^RD&records&HL70126",
            ResponseModality = "R^Real Time^HL70394"
        };

        var message = new Hl7Message();
        message.AddSegment(msh);
        message.AddSegment(qpd);
        message.AddSegment(rcp);
        return message;
    }

    /// <summary>
    /// Parses an RSP (Response) message from CAIR2
    /// </summary>
    /// <param name="hl7Message">The raw HL7 message string</param>
    /// <returns>A <see cref="Cair2QueryResponse"/> object</returns>
    public Cair2QueryResponse ParseRspMessage(string hl7Message)
    {
        var message = _parser.ParseMessage(hl7Message);
        return ParseRspMessage(message);
    }

    /// <summary>
    /// Parses an RSP (Response) message from CAIR2
    /// </summary>
    /// <param name="message">The parsed <see cref="Hl7Message"/></param>
    /// <returns>A <see cref="Cair2QueryResponse"/> object</returns>
    public Cair2QueryResponse ParseRspMessage(Hl7Message message)
    {
        var response = new Cair2QueryResponse
        {
            RawMessage = message,
            Metadata = _cair2Parser.ExtractMessageMetadata(message),
            Patient = _cair2Parser.ExtractPatientInfo(message),
            VaccinationRecords = _cair2Parser.ExtractVaccinationRecords(message)
        };

        var msh = message.GetSegment<MSHSegment>("MSH");
        if (msh != null)
            response.MessageProfileIdentifier = msh.MessageProfileIdentifier;

        var qak = message.GetSegment<QAKSegment>("QAK");
        if (qak != null)
        {
            response.QueryTag = qak.QueryTag;
            response.QueryResponseStatus = qak.QueryResponseStatus;
        }

        var msa = message.GetSegment<MSASegment>("MSA");
        if (msa != null)
            response.AcknowledgmentCode = msa.AcknowledgmentCode;

        if (string.IsNullOrWhiteSpace(response.QueryTag))
        {
            var qpd = message.GetSegment<QPDSegment>("QPD");
            if (qpd != null)
                response.QueryTag = qpd.QueryTag;
        }

        return response;
    }

    private static string BuildQueryName(Cair2QueryProfile profile)
    {
        return profile switch
        {
            Cair2QueryProfile.Z34 => "Z34^Request Immunization History^HL70471",
            Cair2QueryProfile.Z44 => "Z44^Request Immunization History^HL70471",
            _ => throw new ArgumentOutOfRangeException(nameof(profile), profile, "Unsupported CAIR2 query profile.")
        };
    }

    private static string BuildProfileIdentifier(Cair2QueryProfile profile)
    {
        return profile switch
        {
            Cair2QueryProfile.Z34 => "Z34^CDCPHINVS",
            Cair2QueryProfile.Z44 => "Z44^CDCPHINVS",
            _ => throw new ArgumentOutOfRangeException(nameof(profile), profile, "Unsupported CAIR2 query profile.")
        };
    }
}
