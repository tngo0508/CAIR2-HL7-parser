namespace Hl7.Core.CAIR2;

using Hl7.Core.Base;
using Hl7.Core.Segments;

public enum Cair2QueryProfile
{
    Z34,
    Z44
}

public class Cair2QueryParameters
{
    public string SendingApplication { get; set; } = string.Empty;
    public string SendingFacility { get; set; } = string.Empty;
    public string ReceivingApplication { get; set; } = string.Empty;
    public string ReceivingFacility { get; set; } = string.Empty;
    public string MessageControlId { get; set; } = string.Empty;
    public string QueryTag { get; set; } = string.Empty;
    public string PatientIdentifierList { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public string MothersMaidenName { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string AdministrativeSex { get; set; } = string.Empty;
    public string PatientAddress { get; set; } = string.Empty;
    public string PhoneNumberHome { get; set; } = string.Empty;
    public string MultipleBirthIndicator { get; set; } = string.Empty;
    public string BirthOrder { get; set; } = string.Empty;
    public DateTime? MessageDateTime { get; set; }
    public string ProcessingId { get; set; } = "P";
}

public class Cair2QueryResponse
{
    public string QueryTag { get; set; } = string.Empty;
    public string QueryResponseStatus { get; set; } = string.Empty;
    public string AcknowledgmentCode { get; set; } = string.Empty;
    public string MessageProfileIdentifier { get; set; } = string.Empty;
    public MessageMetadata? Metadata { get; set; }
    public PatientInfo? Patient { get; set; }
    public List<VaccinationRecord> VaccinationRecords { get; set; } = [];
    public Hl7Message? RawMessage { get; set; }
}

public class Cair2BidirectionalExchange
{
    private readonly Hl7Parser _parser = new();
    private readonly CAIR2Parser _cair2Parser = new();

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

    public Cair2QueryResponse ParseRspMessage(string hl7Message)
    {
        var message = _parser.ParseMessage(hl7Message);
        return ParseRspMessage(message);
    }

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
