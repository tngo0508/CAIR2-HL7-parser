namespace Hl7.Core.CAIR2;

using Hl7.Core.Base;
using Hl7.Core.Segments;

/// <summary>
/// CAIR2-specific HL7 parser for vaccination data
/// </summary>
public class CAIR2Parser
{
    private readonly Hl7Parser _baseParser = new();

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
            records.Add(new VaccinationRecord
            {
                PatientId = pidSegment.PatientId,
                PatientName = pidSegment.PatientName,
                DateOfBirth = pidSegment.DateOfBirth,
                VaccineCode = rxa.AdministeredCode,
                AdministrationDate = rxa.DateTimeOfAdministration,
                AdministrationSite = rxa.AdministrationSite,
                AdministrationRoute = rxa.AdministrationRoute,
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
    public string PatientId { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string VaccineCode { get; set; } = string.Empty;
    public string AdministrationDate { get; set; } = string.Empty;
    public string AdministrationSite { get; set; } = string.Empty;
    public string AdministrationRoute { get; set; } = string.Empty;
    public string LotNumber { get; set; } = string.Empty;
    public string ExpirationDate { get; set; } = string.Empty;
    public string ManufacturerName { get; set; } = string.Empty;
    public string AdministeringProvider { get; set; } = string.Empty;
    public string CompletionStatus { get; set; } = string.Empty;
    public string ActionCode { get; set; } = string.Empty;
}

/// <summary>
/// Represents patient information extracted from a PID segment
/// </summary>
public class PatientInfo
{
    public string PatientId { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string AdministrativeSex { get; set; } = string.Empty;
    public string Race { get; set; } = string.Empty;
    public string PatientAddress { get; set; } = string.Empty;
    public string PhoneNumberHome { get; set; } = string.Empty;
    public string PrimaryLanguage { get; set; } = string.Empty;
}

/// <summary>
/// Represents message metadata extracted from an MSH segment
/// </summary>
public class MessageMetadata
{
    public string SendingApplication { get; set; } = string.Empty;
    public string SendingFacility { get; set; } = string.Empty;
    public string ReceivingApplication { get; set; } = string.Empty;
    public string ReceivingFacility { get; set; } = string.Empty;
    public string MessageDateTime { get; set; } = string.Empty;
    public string MessageType { get; set; } = string.Empty;
    public string MessageControlId { get; set; } = string.Empty;
    public string ProcessingId { get; set; } = string.Empty;
    public string VersionId { get; set; } = string.Empty;
}
