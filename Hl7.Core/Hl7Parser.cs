namespace Hl7.Core;

using System.Reflection;
using Hl7.Core.Base;
using Hl7.Core.Segments;
using Hl7.Core.Utils;

public class Hl7Parser
{
    private Hl7Separators _separators = new();
    private const string DefaultFieldSeparator = "|";

    public Hl7Parser()
    {
        // Initialize with default separators
        _separators = new Hl7Separators();
    }

    /// <summary>
    /// Parses an HL7 message containing multiple segments
    /// </summary>
    public Hl7Message ParseMessage(string hl7Message)
    {
        if (string.IsNullOrWhiteSpace(hl7Message))
            throw new ArgumentException("HL7 message cannot be null or empty");

        var message = new Hl7Message();
        var lines = hl7Message.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var trimmedLine = line.Trim();

            // MSH segment must be parsed first to extract separators
            if (trimmedLine.StartsWith("MSH"))
            {
                var mshSegment = ParseMSHSegment(trimmedLine);
                message.AddSegment(mshSegment);
                message.MessageVersion = mshSegment.VersionId;
            }
            else
            {
                var segment = ParseSegment(trimmedLine);
                if (segment != null)
                {
                    message.AddSegment(segment);
                }
            }
        }

        return message;
    }

    /// <summary>
    /// Parses the MSH segment and extracts separators
    /// </summary>
    public MSHSegment ParseMSHSegment(string segmentLine)
    {
        if (string.IsNullOrWhiteSpace(segmentLine) || !segmentLine.StartsWith("MSH"))
            throw new ArgumentException("Invalid MSH segment");

        // Extract separators from the MSH segment
        _separators = Hl7Separators.ParseFromMSH(segmentLine);
        
        var fields = SplitFields(segmentLine, _separators.FieldSeparator);
        
        // HL7v2 MSH Special Case: After splitting by |, array indices are:
        // [0] = "MSH", [1] = encoding chars, [2] = SendingApplication, [3] = SendingFacility, ...
        // So we need to use [index+1] to get the right HL7 field
        
        var mshSegment = new MSHSegment
        {
            SegmentId = "MSH",
            EncodingCharacters = _separators.ComponentSeparator.ToString() +
                                _separators.RepetitionSeparator.ToString() +
                                _separators.EscapeCharacter.ToString() +
                                _separators.SubComponentSeparator.ToString(),
            SendingApplication = GetField(fields, 2),      // MSH-3 (array index 2)
            SendingFacility = GetField(fields, 3),         // MSH-4 (array index 3)
            ReceivingApplication = GetField(fields, 4),    // MSH-5 (array index 4)
            ReceivingFacility = GetField(fields, 5),       // MSH-6 (array index 5)
            MessageDateTime = GetField(fields, 6),         // MSH-7 (array index 6)
            Security = GetField(fields, 7),                // MSH-8 (array index 7)
            MessageType = GetField(fields, 8),             // MSH-9 (array index 8)
            MessageControlId = GetField(fields, 9),        // MSH-10 (array index 9)
            ProcessingId = GetField(fields, 10),           // MSH-11 (array index 10)
            VersionId = GetField(fields, 11),              // MSH-12 (array index 11)
            MessageProfileId = GetField(fields, 12),       // MSH-13 (array index 12)
            CountryCode = GetField(fields, 13),            // MSH-14 (array index 13)
            CharacterSet = GetField(fields, 14),           // MSH-15 (array index 14)
            PrincipalLanguageOfMessage = GetField(fields, 15)  // MSH-16 (array index 15)
        };

        mshSegment.Fields = new Dictionary<int, string>();
        for (int i = 1; i < fields.Length; i++)
        {
            mshSegment.Fields[i] = UnescapeField(fields[i]);
        }

        return mshSegment;
    }

    /// <summary>
    /// Parses a single segment line
    /// </summary>
    public Segment? ParseSegment(string segmentLine)
    {
        if (string.IsNullOrWhiteSpace(segmentLine))
            return null;

        var segmentId = segmentLine[..3];
        
        // Handle MSH separately (should be parsed first)
        if (segmentId == "MSH")
        {
            return ParseMSHSegment(segmentLine);
        }

        var fields = SplitFields(segmentLine, _separators.FieldSeparator);
        
        return segmentId switch
        {
            "PID" => ParsePIDSegment(fields),
            "OBR" => ParseOBRSegment(fields),
            "OBX" => ParseOBXSegment(fields),
            "RXA" => ParseRXASegment(fields),
            _ => ParseGenericSegment(segmentId, fields)
        };
    }

    private PIDSegment ParsePIDSegment(string[] fields)
    {
        var segment = new PIDSegment
        {
            SegmentId = "PID",
            SetId = ParseInt(UnescapeField(GetField(fields, 1))),
            PatientId = UnescapeField(GetField(fields, 2)),
            PatientIdentifierList = UnescapeField(GetField(fields, 3)),
            AlternatePatientId = UnescapeField(GetField(fields, 4)),
            PatientName = UnescapeField(GetField(fields, 5)),
            MothersMaidenName = UnescapeField(GetField(fields, 6)),
            DateOfBirth = UnescapeField(GetField(fields, 7)),
            AdministrativeSex = UnescapeField(GetField(fields, 8)),
            Race = UnescapeField(GetField(fields, 9)),
            PatientAddress = UnescapeField(GetField(fields, 11)),      // ✅ FIXED: Was [10], should be [11]
            CountyCode = UnescapeField(GetField(fields, 12)),          // ✅ FIXED: Was [11], should be [12]
            PhoneNumberHome = UnescapeField(GetField(fields, 13)),     // ✅ FIXED: Was [12], should be [13]
            PhoneNumberBusiness = UnescapeField(GetField(fields, 14))  // ✅ FIXED: Was [13], should be [14]
        };

        if (fields.Length > 14)
            segment.PrimaryLanguage = UnescapeField(GetField(fields, 15));

        PopulateSegmentFields(segment, fields);
        return segment;
    }

    private OBRSegment ParseOBRSegment(string[] fields)
    {
        var segment = new OBRSegment
        {
            SegmentId = "OBR",
            SetId = ParseInt(UnescapeField(GetField(fields, 1))),
            PlacerOrderNumber = UnescapeField(GetField(fields, 2)),
            FillerOrderNumber = UnescapeField(GetField(fields, 3)),
            UniversalServiceId = UnescapeField(GetField(fields, 4)),
            Priority = ParseInt(UnescapeField(GetField(fields, 5))),
            RequestedDateTime = UnescapeField(GetField(fields, 6)),
            ObservationDateTime = UnescapeField(GetField(fields, 7)),
            ObservationEndDateTime = UnescapeField(GetField(fields, 8)),
            CollectorsComment = UnescapeField(GetField(fields, 9)),
            OrdererSComments = UnescapeField(GetField(fields, 10)),
            OrdererSName = UnescapeField(GetField(fields, 11)),
            OrdererSAddress = UnescapeField(GetField(fields, 12)),
            OrdererSPhoneNumber = UnescapeField(GetField(fields, 13))
        };

        if (fields.Length > 14)
            segment.OrdererSEmailAddress = UnescapeField(GetField(fields, 14));

        PopulateSegmentFields(segment, fields);
        return segment;
    }

    private OBXSegment ParseOBXSegment(string[] fields)
    {
        var segment = new OBXSegment
        {
            SegmentId = "OBX",
            SetId = ParseInt(UnescapeField(GetField(fields, 1))),
            ValueType = UnescapeField(GetField(fields, 2)),
            ObservationIdentifier = UnescapeField(GetField(fields, 3)),
            ObservationSubId = UnescapeField(GetField(fields, 4)),
            ObservationValue = UnescapeField(GetField(fields, 5)),
            Units = UnescapeField(GetField(fields, 6)),
            ReferenceRange = UnescapeField(GetField(fields, 7)),
            AbnormalFlags = UnescapeField(GetField(fields, 8)),
            Probability = UnescapeField(GetField(fields, 9)),
            NatureOfAbnormalTest = UnescapeField(GetField(fields, 10)),
            ObservationResultStatus = UnescapeField(GetField(fields, 11)),
            DateTimeOfObservation = UnescapeField(GetField(fields, 12)),
            ProducersReference = UnescapeField(GetField(fields, 13))
        };

        if (fields.Length > 14)
            segment.ResponsibleObserver = UnescapeField(GetField(fields, 14));
        if (fields.Length > 15)
            segment.ObservationMethod = UnescapeField(GetField(fields, 15));

        PopulateSegmentFields(segment, fields);
        return segment;
    }

    private RXASegment ParseRXASegment(string[] fields)
    {
        var segment = new RXASegment
        {
            SegmentId = "RXA",
            GiveSubIdCounter = ParseInt(UnescapeField(GetField(fields, 1))),
            AdministrationSubIdCounter = UnescapeField(GetField(fields, 2)),
            DateTimeOfAdministration = UnescapeField(GetField(fields, 3)),
            DateTimeOfAdministrationEnd = UnescapeField(GetField(fields, 4)),
            AdministeredCode = UnescapeField(GetField(fields, 5)),
            AdministeredAmount = UnescapeField(GetField(fields, 6)),
            AdministeredUnits = UnescapeField(GetField(fields, 7)),
            AdministrationNotes = UnescapeField(GetField(fields, 8)),
            AdministeringProvider = UnescapeField(GetField(fields, 9)),
            AdministrationSite = UnescapeField(GetField(fields, 10)),
            AdministrationRoute = UnescapeField(GetField(fields, 11)),
            AdministeredStrength = UnescapeField(GetField(fields, 12)),
            AdministeredStrengthUnits = UnescapeField(GetField(fields, 13)),
            // CAIR2 has an extra field at [14], so Lot Number is at [15]
            SubstanceLotNumber = UnescapeField(GetField(fields, 15)),      // ✅ CORRECT: HBV12345
            SubstanceExpirationDate = UnescapeField(GetField(fields, 16)),
            SubstanceManufacturerName = UnescapeField(GetField(fields, 17)) // ✅ CORRECT: SKB
        };

        if (fields.Length > 17)
            segment.SubstanceRefusalReason = UnescapeField(GetField(fields, 18));
        if (fields.Length > 18)
            segment.Indication = UnescapeField(GetField(fields, 19));
        if (fields.Length > 19)
            segment.RxnAdministrationNotes = UnescapeField(GetField(fields, 20));
        if (fields.Length > 20)
            segment.CompletionStatus = UnescapeField(GetField(fields, 20));  // ✅ CORRECT: CP at position 20
        if (fields.Length > 21)
            segment.ActionCode = UnescapeField(GetField(fields, 21));

        PopulateSegmentFields(segment, fields);
        return segment;
    }

    private Segment ParseGenericSegment(string segmentId, string[] fields)
    {
        var segment = new Segment(segmentId);
        for (int i = 1; i < fields.Length; i++)
        {
            segment.Fields[i - 1] = UnescapeField(fields[i]);
        }
        return segment;
    }

    /// <summary>
    /// Uses reflection to populate segment fields from parsed data using attributes
    /// </summary>
    private void PopulateSegmentFields(Segment segment, string[] fields)
    {
        segment.Fields = new Dictionary<int, string>();
        for (int i = 1; i < fields.Length; i++)
        {
            segment.Fields[i] = UnescapeField(fields[i]);
        }
    }

    private string[] SplitFields(string line, char fieldSeparator)
    {
        return line.Split(fieldSeparator);
    }

    private string GetField(string[] fields, int index)
    {
        return index >= 0 && index < fields.Length ? fields[index] : string.Empty;
    }

    private int ParseInt(string value)
    {
        return int.TryParse(value, out var result) ? result : 0;
    }

    private string UnescapeField(string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        return Hl7FieldHelper.UnescapeField(value, _separators);
    }
}