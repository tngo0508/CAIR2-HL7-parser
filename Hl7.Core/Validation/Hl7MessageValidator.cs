namespace Hl7.Core.Validation;

using Hl7.Core.Base;
using Hl7.Core.Segments;

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
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = [];
        public List<string> Warnings { get; set; } = [];

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
}
