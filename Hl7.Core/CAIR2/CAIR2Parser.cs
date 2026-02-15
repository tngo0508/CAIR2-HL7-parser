using Hl7.Core.Base;
using Hl7.Core.Segments;

namespace Hl7.Core.CAIR2;

/// <summary>
/// CAIR2-specific HL7 parser for vaccination data
/// </summary>
public class CAIR2Parser
{
    private readonly Hl7Parser _baseParser = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CAIR2Parser"/> class
    /// </summary>
    public CAIR2Parser() { }

    /// <summary>
    /// Parses a complete CAIR2 vaccination message
    /// </summary>
    public Hl7Message ParseVaccinationMessage(string hl7Message)
    {
        if (string.IsNullOrWhiteSpace(hl7Message))
            throw new ArgumentException("HL7 message cannot be null or empty", nameof(hl7Message));
            
        var message = _baseParser.ParseMessage(hl7Message);
        return message;
    }

    /// <summary>
    /// Validates that a message contains required CAIR2 segments
    /// </summary>
    public bool ValidateCAIR2Message(Hl7Message message)
    {
        if (message == null)
            return false;

        // Must have MSH segment
        if (message.GetSegment<MSHSegment>("MSH") == null)
            return false;

        // Must have PID segment
        if (message.GetSegment<PIDSegment>("PID") == null)
            return false;

        // Must have at least one RXA segment for vaccines
        if (message.GetSegments<RXASegment>("RXA").Count == 0)
            return false;

        return true;
    }

    /// <summary>
    /// Extracts vaccination records from a message
    /// </summary>
    public List<VaccinationRecord> ExtractVaccinationRecords(Hl7Message message)
    {
        var records = new List<VaccinationRecord>();

        var pidSegment = message.GetSegment<PIDSegment>("PID");
        if (pidSegment == null)
            return records;

        var rxaSegments = message.GetSegments<RXASegment>("RXA");

        foreach (var rxa in rxaSegments)
        {
            var rxr = FindRXRForRXA(message, rxa);

            records.Add(new VaccinationRecord
            {
                PatientId = pidSegment.PatientId,
                PatientName = pidSegment.PatientName,
                DateOfBirth = pidSegment.DateOfBirth,
                VaccineCode = rxa.AdministeredCode,
                AdministrationDate = rxa.DateTimeOfAdministration,
                AdministeredAtLocation = rxa.AdministeredAtLocation,
                AdministrationRoute = rxr?.Route ?? string.Empty,
                AdministrationSite = rxr?.AdministrationSite ?? string.Empty,
                LotNumber = rxa.SubstanceLotNumber,
                ExpirationDate = rxa.SubstanceExpirationDate,
                ManufacturerName = rxa.SubstanceManufacturerName,
                AdministeringProvider = rxa.AdministeringProvider,
                CompletionStatus = rxa.CompletionStatus,
                ActionCode = rxa.ActionCode
            });
        }

        return records;
    }

    private RXRSegment? FindRXRForRXA(Hl7Message message, RXASegment rxa)
    {
        var rxaIndex = message.Segments.IndexOf(rxa);
        if (rxaIndex < 0)
            return null;

        for (int i = rxaIndex + 1; i < message.Segments.Count; i++)
        {
            if (message.Segments[i] is RXASegment)
                break;

            if (message.Segments[i] is RXRSegment rxrSegment)
                return rxrSegment;
        }

        return null;
    }

    /// <summary>
    /// Extracts patient information from a message
    /// </summary>
    public PatientInfo? ExtractPatientInfo(Hl7Message message)
    {
        var pidSegment = message.GetSegment<PIDSegment>("PID");
        if (pidSegment == null)
            return null;

        return new PatientInfo
        {
            PatientId = pidSegment.PatientId,
            PatientName = pidSegment.PatientName,
            DateOfBirth = pidSegment.DateOfBirth,
            AdministrativeSex = pidSegment.AdministrativeSex,
            Race = pidSegment.Race,
            PatientAddress = pidSegment.PatientAddress,
            PhoneNumberHome = pidSegment.PhoneNumberHome,
            PrimaryLanguage = pidSegment.PrimaryLanguage
        };
    }

    /// <summary>
    /// Gets message metadata from MSH segment
    /// </summary>
    public MessageMetadata? ExtractMessageMetadata(Hl7Message message)
    {
        var mshSegment = message.GetSegment<MSHSegment>("MSH");
        if (mshSegment == null)
            return null;

        return new MessageMetadata
        {
            SendingApplication = mshSegment.SendingApplication,
            SendingFacility = mshSegment.SendingFacility,
            ReceivingApplication = mshSegment.ReceivingApplication,
            ReceivingFacility = mshSegment.ReceivingFacility,
            MessageDateTime = mshSegment.MessageDateTime,
            MessageType = mshSegment.MessageType,
            MessageControlId = mshSegment.MessageControlId,
            ProcessingId = mshSegment.ProcessingId,
            VersionId = mshSegment.VersionId
        };
    }
}

/// <summary>
/// Represents a vaccination record extracted from an RXA segment
/// </summary>
public class VaccinationRecord
{
    /// <summary>
    /// Gets or sets the patient identifier
    /// </summary>
    public string PatientId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient name
    /// </summary>
    public string PatientName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient's date of birth
    /// </summary>
    public string DateOfBirth { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the vaccine code
    /// </summary>
    public string VaccineCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the administration date
    /// </summary>
    public string AdministrationDate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the location where the vaccine was administered
    /// </summary>
    public string AdministeredAtLocation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the administration site
    /// </summary>
    public string AdministrationSite { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the administration route
    /// </summary>
    public string AdministrationRoute { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the substance lot number
    /// </summary>
    public string LotNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the substance expiration date
    /// </summary>
    public string ExpirationDate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the substance manufacturer name
    /// </summary>
    public string ManufacturerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the administering provider
    /// </summary>
    public string AdministeringProvider { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the completion status
    /// </summary>
    public string CompletionStatus { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the action code
    /// </summary>
    public string ActionCode { get; set; } = string.Empty;
}

/// <summary>
/// Represents patient information extracted from a PID segment
/// </summary>
public class PatientInfo
{
    /// <summary>
    /// Gets or sets the patient identifier
    /// </summary>
    public string PatientId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient name
    /// </summary>
    public string PatientName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient's date of birth
    /// </summary>
    public string DateOfBirth { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient's sex
    /// </summary>
    public string AdministrativeSex { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient's race
    /// </summary>
    public string Race { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient's address
    /// </summary>
    public string PatientAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient's home phone number
    /// </summary>
    public string PhoneNumberHome { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the patient's primary language
    /// </summary>
    public string PrimaryLanguage { get; set; } = string.Empty;
}

/// <summary>
/// Represents message metadata extracted from an MSH segment
/// </summary>
public class MessageMetadata
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
    /// Gets or sets the date and time of the message
    /// </summary>
    public string MessageDateTime { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message type
    /// </summary>
    public string MessageType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message control identifier
    /// </summary>
    public string MessageControlId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the processing identifier
    /// </summary>
    public string ProcessingId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the version identifier
    /// </summary>
    public string VersionId { get; set; } = string.Empty;
}
