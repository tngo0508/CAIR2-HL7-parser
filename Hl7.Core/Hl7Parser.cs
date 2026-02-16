using System.Reflection;
using Hl7.Core.Base;
using Hl7.Core.Common;
using Hl7.Core.Segments;
using Hl7.Core.Utils;
using Hl7.Core.Types;

namespace Hl7.Core;

/// <summary>
/// Parses and manipulates HL7 messages
/// </summary>
public class Hl7Parser
{
    private Hl7Separators _separators = new();
    private const string DefaultFieldSeparator = "|";

    /// <summary>
    /// Initializes a new instance of the <see cref="Hl7Parser"/> class
    /// </summary>
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
        if (string.IsNullOrEmpty(hl7Message))
            return new Hl7Message();

        var message = new Hl7Message();
        var lines = hl7Message.Split(new[] { "\r\n", "\r", "\n", "\v", "\f", "\x1C" }, StringSplitOptions.RemoveEmptyEntries);

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
                message.Separators = _separators;
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
        
        // MSH is special. The field separator is the 4th character (index 3).
        var fieldSeparator = segmentLine[3];
        
        // In HL7, MSH is the only segment where the segment ID is followed immediately by the field separator,
        // and that separator itself is MSH-1.
        // segmentLine: "MSH|^~\\&|..."
        // fields: ["MSH", "^~\\&", "...", ...]
        var fields = segmentLine.Split(fieldSeparator);
        
        var mshSegment = new MSHSegment
        {
            SegmentId = "MSH",
            EncodingCharacters = GetField(fields, 1),                // MSH-2
            SendingApplication = GetField(fields, 2),                // MSH-3
            SendingFacility = GetField(fields, 3),                   // MSH-4
            ReceivingApplication = GetField(fields, 4),              // MSH-5
            ReceivingFacility = GetField(fields, 5),                 // MSH-6
            MessageDateTime = GetField(fields, 6),                   // MSH-7
            Security = GetField(fields, 7),                          // MSH-8
            MessageType = GetField(fields, 8),                       // MSH-9
            MessageControlId = GetField(fields, 9),                  // MSH-10
            ProcessingId = GetField(fields, 10),                     // MSH-11
            VersionId = GetField(fields, 11),                        // MSH-12
            SequenceNumber = GetField(fields, 12),                   // MSH-13
            ContinuationPointer = GetField(fields, 13),              // MSH-14
            AcceptAcknowledgmentType = GetField(fields, 14),         // MSH-15
            ApplicationAcknowledgmentType = GetField(fields, 15),    // MSH-16
            CountryCode = GetField(fields, 16),                      // MSH-17
            CharacterSet = GetField(fields, 17),                     // MSH-18
            PrincipalLanguageOfMessage = GetField(fields, 18),       // MSH-19
            AlternateCharacterSetHandlingScheme = GetField(fields, 19), // MSH-20
            MessageProfileIdentifier = GetField(fields, 20)          // MSH-21
        };

        mshSegment.Fields = new Dictionary<int, string>();
        mshSegment.Fields[1] = fieldSeparator.ToString(); // MSH-1 is the separator
        for (int i = 1; i < fields.Length; i++)
        {
            mshSegment.Fields[i + 1] = fields[i];
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

        if (segmentLine.Length < 3)
            return ParseGenericSegment(segmentLine, Array.Empty<string>());

        var segmentId = segmentLine[..3];
        
        // Handle MSH separately (should be parsed first)
        if (segmentId == "MSH")
        {
            return ParseMSHSegment(segmentLine);
        }

        var fields = SplitFields(segmentLine, _separators.FieldSeparator);
        
        return segmentId switch
        {
            "PID" => ParseSegmentByAttributes<PIDSegment>(segmentId, fields),
            "OBR" => ParseSegmentByAttributes<OBRSegment>(segmentId, fields),
            "OBX" => ParseSegmentByAttributes<OBXSegment>(segmentId, fields),
            "RXA" => ParseRXASegment(fields),
            "RXR" => ParseSegmentByAttributes<RXRSegment>(segmentId, fields),
            "ORC" => ParseSegmentByAttributes<ORCSegment>(segmentId, fields),
            "PD1" => ParseSegmentByAttributes<PD1Segment>(segmentId, fields),
            "NK1" => ParseSegmentByAttributes<NK1Segment>(segmentId, fields),
            "QPD" => ParseSegmentByAttributes<QPDSegment>(segmentId, fields),
            "RCP" => ParseSegmentByAttributes<RCPSegment>(segmentId, fields),
            "QAK" => ParseSegmentByAttributes<QAKSegment>(segmentId, fields),
            "MSA" => ParseSegmentByAttributes<MSASegment>(segmentId, fields),
            "ERR" => ParseSegmentByAttributes<ERRSegment>(segmentId, fields),
            _ => ParseGenericSegment(segmentId, fields)
        };
    }

    private TSegment ParseSegmentByAttributes<TSegment>(string segmentId, string[] fields)
        where TSegment : Segment, new()
    {
        var segment = new TSegment();
        if (string.IsNullOrWhiteSpace(segment.SegmentId))
            segment.SegmentId = segmentId;

        var properties = segment.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => new { Property = p, Attribute = p.GetCustomAttribute<DataElementAttribute>() })
            .Where(p => p.Attribute != null)
            .ToList();

        foreach (var entry in properties)
        {
            var position = entry.Attribute!.Position;
            var rawValue = GetField(fields, position);

            if (entry.Property.PropertyType == typeof(int))
            {
                entry.Property.SetValue(segment, ParseInt(UnescapeField(rawValue)));
            }
            else if (entry.Property.PropertyType == typeof(string))
            {
                entry.Property.SetValue(segment, UnescapeField(rawValue));
            }
            else if (typeof(CompositeDataType).IsAssignableFrom(entry.Property.PropertyType))
            {
                var composite = (CompositeDataType)Activator.CreateInstance(entry.Property.PropertyType)!;
                composite.Parse(rawValue, _separators.ComponentSeparator, _separators.SubComponentSeparator);
                
                // Unescape components and subcomponents
                for (int i = 0; i < composite.Components.Count; i++)
                {
                    composite.Components[i] = UnescapeField(composite.Components[i]);
                    if (composite.SubComponents.ContainsKey(i))
                    {
                        for (int j = 0; j < composite.SubComponents[i].Count; j++)
                        {
                            composite.SubComponents[i][j] = UnescapeField(composite.SubComponents[i][j]);
                        }
                    }
                }
                
                entry.Property.SetValue(segment, composite);
            }
        }

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
            AdministeredCode = ParseComposite<CE>(GetField(fields, 5)),
            AdministeredAmount = UnescapeField(GetField(fields, 6)),
            AdministeredUnits = ParseComposite<CE>(GetField(fields, 7)),
            AdministrationNotes = UnescapeField(GetField(fields, 9)),
            AdministeringProvider = ParseComposite<XPN>(GetField(fields, 10)),
            AdministeredAtLocation = UnescapeField(GetField(fields, 11)),
            AdministeredPer = UnescapeField(GetField(fields, 12)),
            AdministeredStrength = UnescapeField(GetField(fields, 13)),
            AdministeredStrengthUnits = UnescapeField(GetField(fields, 14)),
            SubstanceLotNumber = UnescapeField(GetField(fields, 15)),
            SubstanceExpirationDate = UnescapeField(GetField(fields, 16)),
            SubstanceManufacturerName = ParseComposite<CE>(GetField(fields, 17))
        };

        if (fields.Length > 17)
            segment.SubstanceRefusalReason = ParseComposite<CE>(GetField(fields, 18));
        if (fields.Length > 18)
            segment.Indication = UnescapeField(GetField(fields, 19));
        if (fields.Length > 19)
            segment.CompletionStatus = UnescapeField(GetField(fields, 20));
        if (fields.Length > 20)
            segment.ActionCode = UnescapeField(GetField(fields, 21));

        var field12 = GetField(fields, 12);
        var field13 = GetField(fields, 13);
        var field14 = GetField(fields, 14);
        var field15 = GetField(fields, 15);

        if (!string.IsNullOrWhiteSpace(field12) && LooksLikeDate(field13) && string.IsNullOrWhiteSpace(field14))
        {
            segment.SubstanceLotNumber = UnescapeField(field12);
            segment.SubstanceExpirationDate = UnescapeField(field13);
            if (!string.IsNullOrWhiteSpace(field15))
                segment.SubstanceManufacturerName = ParseComposite<CE>(field15);
        }
        else if (string.IsNullOrWhiteSpace(segment.SubstanceLotNumber))
        {
            if (!string.IsNullOrWhiteSpace(field12))
            {
                segment.SubstanceLotNumber = UnescapeField(field12);
                if (string.IsNullOrWhiteSpace(segment.SubstanceExpirationDate))
                    segment.SubstanceExpirationDate = UnescapeField(field13);
                if (string.IsNullOrWhiteSpace(segment.SubstanceManufacturerName))
                    segment.SubstanceManufacturerName = ParseComposite<CE>(field15);
            }
        }

        PopulateSegmentFields(segment, fields);
        return segment;
    }

    private TComposite ParseComposite<TComposite>(string value) where TComposite : CompositeDataType, new()
    {
        var composite = new TComposite();
        composite.Parse(value, _separators.ComponentSeparator, _separators.SubComponentSeparator);
        
        for (int i = 0; i < composite.Components.Count; i++)
        {
            composite.Components[i] = UnescapeField(composite.Components[i]);
            if (composite.SubComponents.ContainsKey(i))
            {
                for (int j = 0; j < composite.SubComponents[i].Count; j++)
                {
                    composite.SubComponents[i][j] = UnescapeField(composite.SubComponents[i][j]);
                }
            }
        }
        
        return composite;
    }

    private static bool LooksLikeDate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (value.Length != 8 && value.Length != 14)
            return false;

        foreach (var ch in value)
        {
            if (!char.IsDigit(ch))
                return false;
        }

        return true;
    }

    private Segment ParseGenericSegment(string segmentId, string[] fields)
    {
        var segment = new Segment(string.IsNullOrWhiteSpace(segmentId) ? "???" : segmentId);
        PopulateSegmentFields(segment, fields);
        return segment;
    }

    /// <summary>
    /// Uses reflection to populate segment fields from parsed data using attributes
    /// </summary>
    private void PopulateSegmentFields(Segment segment, string[] fields)
    {
        segment.Fields = new Dictionary<int, string>();
        if (fields == null) return;
        
        for (int i = 1; i < fields.Length; i++)
        {
            segment.Fields[i] = fields[i];
        }
    }

    private string[] SplitFields(string line, char fieldSeparator)
    {
        if (string.IsNullOrEmpty(line))
            return Array.Empty<string>();
        return line.Split(fieldSeparator);
    }

    private string GetField(string[] fields, int index)
    {
        if (fields == null || index < 0 || index >= fields.Length)
            return string.Empty;
        return fields[index];
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
