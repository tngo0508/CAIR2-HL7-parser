using System.Reflection;
using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Segments;
using Hl7.Core.Utils;

namespace Hl7.Core.Validation;

/// <summary>
/// Validates HL7 messages for compliance with HL7v2 standards
/// </summary>
public class Hl7MessageValidator
{
    /// <summary>
    /// Validation result containing errors and warnings
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the message is valid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets the list of validation errors
        /// </summary>
        public List<string> Errors { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of validation warnings
        /// </summary>
        public List<string> Warnings { get; set; } = [];

        /// <summary>
        /// Returns a string representation of the validation result
        /// </summary>
        /// <returns>A formatted string with errors and warnings</returns>
        public override string ToString()
        {
            var result = new System.Text.StringBuilder();
            result.AppendLine($"Valid: {IsValid}");
            
            if (Errors.Count > 0)
            {
                result.AppendLine("Errors:");
                foreach (var error in Errors)
                {
                    result.AppendLine($"  - {error}");
                }
            }

            if (Warnings.Count > 0)
            {
                result.AppendLine("Warnings:");
                foreach (var warning in Warnings)
                {
                    result.AppendLine($"  - {warning}");
                }
            }

            return result.ToString();
        }
    }

    /// <summary>
    /// Validates an HL7 message for basic compliance
    /// </summary>
    public ValidationResult Validate(Hl7Message message)
    {
        var result = new ValidationResult();

        if (message == null)
        {
            result.IsValid = false;
            result.Errors.Add("Message is null");
            return result;
        }

        if (message.Segments.Count == 0)
        {
            result.IsValid = false;
            result.Errors.Add("Message contains no segments");
            return result;
        }

        // MSH segment must be first
        if (!(message.Segments[0] is MSHSegment))
        {
            result.IsValid = false;
            result.Errors.Add("First segment must be MSH (Message Header)");
        }

        // Check for required MSH fields
        var mshSegment = message.GetSegment<MSHSegment>("MSH");
        if (mshSegment != null)
        {
            ValidateMSHSegment(mshSegment, result);
        }

        // Check for required segments based on message type
        ValidateSegmentHierarchy(message, result);

        foreach (var segment in message.Segments)
        {
            ValidateSegmentDataElements(segment, result, strictRequired: false);
        }

        result.IsValid = result.Errors.Count == 0;
        return result;
    }

    /// <summary>
    /// Validates CAIR2 bidirectional (QBP/RSP) messages.
    /// </summary>
    public ValidationResult ValidateCair2Bidirectional(Hl7Message message, bool strict = true)
    {
        var result = new ValidationResult();

        if (message == null)
        {
            result.IsValid = false;
            result.Errors.Add("Message is null");
            return result;
        }

        var msh = message.GetSegment<MSHSegment>("MSH");
        if (msh == null)
        {
            result.IsValid = false;
            result.Errors.Add("Missing MSH segment");
            return result;
        }

        var messageType = msh.MessageType ?? string.Empty;
        if (messageType.StartsWith("QBP", StringComparison.OrdinalIgnoreCase))
        {
            ValidateCair2Qbp(message, msh, result, strict);
        }
        else if (messageType.StartsWith("RSP", StringComparison.OrdinalIgnoreCase))
        {
            ValidateCair2Rsp(message, msh, result, strict);
        }
        else
        {
            AddIssue(result, strict, $"Unsupported message type for CAIR2 bidirectional validation: '{messageType}'");
        }

        foreach (var segment in message.Segments)
        {
            ValidateSegmentDataElements(segment, result, strictRequired: strict);
        }

        result.IsValid = result.Errors.Count == 0;
        return result;
    }

    private void ValidateMSHSegment(MSHSegment msh, ValidationResult result)
    {
        if (string.IsNullOrWhiteSpace(msh.VersionId))
        {
            result.Warnings.Add("MSH-12: VersionId is empty");
        }

        if (string.IsNullOrWhiteSpace(msh.MessageType))
        {
            result.Errors.Add("MSH-9: MessageType is required");
        }

        if (string.IsNullOrWhiteSpace(msh.MessageControlId))
        {
            result.Errors.Add("MSH-10: MessageControlId is required");
        }

        if (string.IsNullOrWhiteSpace(msh.ProcessingId))
        {
            result.Warnings.Add("MSH-11: ProcessingId is empty");
        }
    }

    private void ValidateSegmentHierarchy(Hl7Message message, ValidationResult result)
    {
        var mshSegment = message.GetSegment<MSHSegment>("MSH");
        if (mshSegment == null)
            return;

        // For VXU messages, check vaccination-specific segments
        if (mshSegment.MessageType?.StartsWith("VXU") == true)
        {
            ValidateVXUMessage(message, result);
        }
    }

    private void ValidateCair2Qbp(Hl7Message message, MSHSegment msh, ValidationResult result, bool strict)
    {
        if (!string.Equals(msh.MessageType, "QBP^Q11^QBP_Q11", StringComparison.OrdinalIgnoreCase))
        {
            AddIssue(result, strict, "MSH-9 must be QBP^Q11^QBP_Q11 for CAIR2 queries");
        }

        ValidateCair2MshAckFields(msh, result, strict);

        var profileCode = GetComponent(msh.MessageProfileIdentifier, 0);
        if (!IsAny(profileCode, "Z34", "Z44"))
        {
            AddIssue(result, strict, "MSH-21 must be Z34^CDCPHINVS or Z44^CDCPHINVS");
        }

        var qpd = message.GetSegment<QPDSegment>("QPD");
        if (qpd == null)
        {
            AddIssue(result, strict, "QPD segment is required for CAIR2 QBP");
        }
        else
        {
            var queryNameCode = GetComponent(qpd.MessageQueryName, 0);
            if (!IsAny(queryNameCode, "Z34", "Z44"))
            {
                AddIssue(result, strict, "QPD-1 must be Z34^... or Z44^...");
            }
            else if (!string.IsNullOrWhiteSpace(profileCode) &&
                     !string.Equals(queryNameCode, profileCode, StringComparison.OrdinalIgnoreCase))
            {
                AddIssue(result, strict, "QPD-1 must match the MSH-21 profile identifier");
            }

            if (string.IsNullOrWhiteSpace(qpd.QueryTag))
                AddIssue(result, strict, "QPD-2 (Query Tag) is required");

            if (string.IsNullOrWhiteSpace(qpd.PatientName))
                AddIssue(result, strict, "QPD-4 (Patient Name) is required");

            if (string.IsNullOrWhiteSpace(qpd.DateOfBirth))
                AddIssue(result, strict, "QPD-6 (Date of Birth) is required");

            if (string.IsNullOrWhiteSpace(qpd.AdministrativeSex))
            {
                AddIssue(result, strict, "QPD-7 (Sex) is required");
            }
            else if (!IsAny(qpd.AdministrativeSex, "F", "M", "O", "U", "T"))
            {
                AddIssue(result, strict, "QPD-7 (Sex) must be F, M, O, U, or T");
            }

            if (string.IsNullOrWhiteSpace(qpd.PatientAddress))
                AddIssue(result, strict, "QPD-8 (Patient Address) is required");

            if (string.Equals(qpd.MultipleBirthIndicator, "Y", StringComparison.OrdinalIgnoreCase) &&
                string.IsNullOrWhiteSpace(qpd.BirthOrder))
            {
                AddIssue(result, strict, "QPD-11 (Birth Order) is required when QPD-10 is Y");
            }
        }

        var rcp = message.GetSegment<RCPSegment>("RCP");
        if (rcp == null)
        {
            AddIssue(result, strict, "RCP segment is required for CAIR2 QBP");
        }
        else
        {
            if (!string.Equals(rcp.QueryPriority, "I", StringComparison.OrdinalIgnoreCase))
                AddIssue(result, strict, "RCP-1 must be I (Immediate)");

            if (string.IsNullOrWhiteSpace(rcp.QuantityLimitedRequest))
            {
                AddIssue(result, strict, "RCP-2 must be populated");
            }
            else
            {
                var rcpUnit = GetComponent(rcp.QuantityLimitedRequest, 1);
                if (!string.Equals(rcpUnit, "RD", StringComparison.OrdinalIgnoreCase))
                    AddIssue(result, strict, "RCP-2 must use RD (records) for the unit");
            }

            var responseModalityCode = GetComponent(rcp.ResponseModality, 0);
            if (!string.Equals(responseModalityCode, "R", StringComparison.OrdinalIgnoreCase))
                AddIssue(result, strict, "RCP-3 must be R (Response on demand)");
        }
    }

    private void ValidateCair2Rsp(Hl7Message message, MSHSegment msh, ValidationResult result, bool strict)
    {
        if (!string.Equals(msh.MessageType, "RSP^K11^RSP_K11", StringComparison.OrdinalIgnoreCase))
        {
            AddIssue(result, strict, "MSH-9 must be RSP^K11^RSP_K11 for CAIR2 responses");
        }

        ValidateCair2MshAckFields(msh, result, strict);

        var profileCode = GetComponent(msh.MessageProfileIdentifier, 0);
        if (!IsAny(profileCode, "Z31", "Z32", "Z33", "Z42"))
        {
            AddIssue(result, strict, "MSH-21 must be Z31, Z32, Z33, or Z42 for CAIR2 responses");
        }

        var msa = message.GetSegment<MSASegment>("MSA");
        if (msa == null)
        {
            AddIssue(result, strict, "MSA segment is required for CAIR2 responses");
        }
        else
        {
            if (!IsAny(msa.AcknowledgmentCode, "AA", "AE", "AR"))
                AddIssue(result, strict, "MSA-1 must be AA, AE, or AR");

            if (string.IsNullOrWhiteSpace(msa.MessageControlId))
                AddIssue(result, strict, "MSA-2 (Message Control ID) is required");
        }

        var qak = message.GetSegment<QAKSegment>("QAK");
        if (qak == null)
        {
            AddIssue(result, strict, "QAK segment is required for CAIR2 responses");
        }
        else
        {
            if (string.IsNullOrWhiteSpace(qak.QueryTag))
                AddIssue(result, strict, "QAK-1 (Query Tag) is required");

            if (!IsAny(qak.QueryResponseStatus, "OK", "NF", "TM", "PD", "AE", "AR"))
                AddIssue(result, strict, "QAK-2 must be OK, NF, TM, PD, AE, or AR");
        }

        var qpd = message.GetSegment<QPDSegment>("QPD");
        if (qpd == null)
        {
            AddIssue(result, strict, "QPD segment is required for CAIR2 responses");
        }
        else if (qak != null && !string.Equals(qpd.QueryTag, qak.QueryTag, StringComparison.OrdinalIgnoreCase))
        {
            result.Warnings.Add("QPD-2 should match QAK-1 in CAIR2 responses");
        }
    }

    private void ValidateCair2MshAckFields(MSHSegment msh, ValidationResult result, bool strict)
    {
        if (!string.Equals(msh.ProcessingId, "P", StringComparison.OrdinalIgnoreCase))
            AddIssue(result, strict, "MSH-11 must be P");

        if (!string.Equals(msh.VersionId, "2.5.1", StringComparison.OrdinalIgnoreCase))
            AddIssue(result, strict, "MSH-12 must be 2.5.1");

        if (!string.Equals(msh.AcceptAcknowledgmentType, "ER", StringComparison.OrdinalIgnoreCase))
            AddIssue(result, strict, "MSH-15 must be ER");

        if (!string.Equals(msh.ApplicationAcknowledgmentType, "AL", StringComparison.OrdinalIgnoreCase))
            AddIssue(result, strict, "MSH-16 must be AL");
    }

    private void AddIssue(ValidationResult result, bool strict, string message)
    {
        if (strict)
            result.Errors.Add(message);
        else
            result.Warnings.Add(message);
    }

    private static string GetComponent(string value, int componentIndex)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var components = value.Split('^');
        return componentIndex >= 0 && componentIndex < components.Length ? components[componentIndex] : string.Empty;
    }

    private static bool IsAny(string value, params string[] allowed)
    {
        foreach (var option in allowed)
        {
            if (string.Equals(value, option, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    private void ValidateVXUMessage(Hl7Message message, ValidationResult result)
    {
        // VXU messages should have PID segment
        if (message.GetSegment<PIDSegment>("PID") == null)
        {
            result.Warnings.Add("VXU message should contain PID (Patient Identification) segment");
        }

        // VXU messages should have at least one RXA segment
        var rxaSegments = message.GetSegments<RXASegment>("RXA");
        if (rxaSegments.Count == 0)
        {
            result.Warnings.Add("VXU message should contain RXA (Pharmacy/Vaccine Administration) segments");
        }
    }

    /// <summary>
    /// Validates a specific segment
    /// </summary>
    public ValidationResult ValidateSegment(Segment segment)
    {
        var result = new ValidationResult();

        if (segment == null)
        {
            result.IsValid = false;
            result.Errors.Add("Segment is null");
            return result;
        }

        if (string.IsNullOrWhiteSpace(segment.SegmentId))
        {
            result.IsValid = false;
            result.Errors.Add("Segment ID is required");
            return result;
        }

        switch (segment)
        {
            case PIDSegment pidSegment:
                ValidatePIDSegment(pidSegment, result);
                break;
            case RXASegment rxaSegment:
                ValidateRXASegment(rxaSegment, result);
                break;
            case OBXSegment obxSegment:
                ValidateOBXSegment(obxSegment, result);
                break;
        }

        ValidateSegmentDataElements(segment, result, strictRequired: false);

        result.IsValid = result.Errors.Count == 0;
        return result;
    }

    private void ValidatePIDSegment(PIDSegment pid, ValidationResult result)
    {
        if (string.IsNullOrWhiteSpace(pid.PatientId))
        {
            result.Warnings.Add("PID-2: PatientId is recommended");
        }

        if (string.IsNullOrWhiteSpace(pid.PatientName))
        {
            result.Warnings.Add("PID-5: PatientName is recommended");
        }

        if (string.IsNullOrWhiteSpace(pid.DateOfBirth))
        {
            result.Warnings.Add("PID-7: DateOfBirth is recommended");
        }
    }

    private void ValidateRXASegment(RXASegment rxa, ValidationResult result)
    {
        if (string.IsNullOrWhiteSpace(rxa.DateTimeOfAdministration))
        {
            result.Warnings.Add("RXA-3: DateTimeOfAdministration is recommended");
        }

        if (string.IsNullOrWhiteSpace(rxa.AdministeredCode))
        {
            result.Errors.Add("RXA-5: AdministeredCode is required");
        }
    }

    private void ValidateOBXSegment(OBXSegment obx, ValidationResult result)
    {
        if (string.IsNullOrWhiteSpace(obx.ValueType))
        {
            result.Errors.Add("OBX-2: ValueType is required");
        }

        if (string.IsNullOrWhiteSpace(obx.ObservationIdentifier))
        {
            result.Errors.Add("OBX-3: ObservationIdentifier is required");
        }
    }

    private void ValidateSegmentDataElements(Segment segment, ValidationResult result, bool strictRequired)
    {
        var properties = segment.GetType()
            .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .Select(p => new { Property = p, Attribute = p.GetCustomAttribute<DataElementAttribute>() })
            .Where(p => p.Attribute != null)
            .ToList();

        foreach (var entry in properties)
        {
            var position = entry.Attribute!.Position;
            var name = string.IsNullOrWhiteSpace(entry.Attribute.Name) ? entry.Property.Name : entry.Attribute.Name;
            var fieldIndex = segment is MSHSegment ? position - 1 : position;
            var fieldValue = GetFieldValue(segment, fieldIndex);

            switch (entry.Attribute.Status)
            {
                case ElementUsage.Required:
                    if (string.IsNullOrWhiteSpace(fieldValue))
                        AddIssue(result, strictRequired, $"{segment.SegmentId}-{position}: {name} is required");
                    break;
                case ElementUsage.NotSupported:
                    if (!string.IsNullOrWhiteSpace(fieldValue))
                        result.Warnings.Add($"{segment.SegmentId}-{position}: {name} is not supported");
                    break;
            }
        }
    }

    private static string GetFieldValue(Segment segment, int index)
    {
        if (index <= 0)
            return string.Empty;

        return segment.Fields.TryGetValue(index, out var value) ? value : string.Empty;
    }
}
