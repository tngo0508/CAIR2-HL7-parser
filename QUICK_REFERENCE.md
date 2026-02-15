# HL7v2 Parser - Quick Reference Guide

## Quick Start

### 1. Parse a Complete Message

```csharp
var parser = new Hl7Parser();
var message = parser.ParseMessage(hl7String);

var msh = message.GetSegment<MSHSegment>("MSH");
var pid = message.GetSegment<PIDSegment>("PID");
var rxa = message.GetSegment<RXASegment>("RXA");
```

### 2. CAIR2 Vaccination Parsing

```csharp
var cair2Parser = new CAIR2Parser();
var message = cair2Parser.ParseVaccinationMessage(hl7String);
var records = cair2Parser.ExtractVaccinationRecords(message);
```

### 3. Validate Message

```csharp
var validator = new Hl7MessageValidator();
var result = validator.Validate(message);
if (!result.IsValid)
{
    foreach (var error in result.Errors)
        Console.WriteLine(error);
}
```

## Common Operations

### Get Segment by Type
```csharp
var segment = message.GetSegment<PIDSegment>("PID");
```

### Get Multiple Segments
```csharp
var segments = message.GetSegments<RXASegment>("RXA");
```

### Access Segment Fields
```csharp
var patientName = pidSegment.PatientName;
var dateOfBirth = pidSegment.DateOfBirth;
```

### Parse Composite Field
```csharp
var composite = Hl7FieldHelper.ParseComposite("90658^INFLUENZA VACCINE", separators);
var code = composite.GetComponent(0);      // 90658
var desc = composite.GetComponent(1);      // INFLUENZA VACCINE
```

### Escape/Unescape Fields
```csharp
var escaped = Hl7FieldHelper.EscapeField(value, separators);
var unescaped = Hl7FieldHelper.UnescapeField(value, separators);
```

### Serialize Message
```csharp
var serializer = new Hl7MessageSerializer(separators);
var hl7String = serializer.Serialize(message);
```

## Data Structures

### Vaccination Record
```csharp
public class VaccinationRecord
{
    public string PatientId { get; set; }
    public string PatientName { get; set; }
    public string VaccineCode { get; set; }
    public string AdministrationDate { get; set; }
    public string AdministrationSite { get; set; }
    public string LotNumber { get; set; }
    // ... more fields
}
```

### Patient Info
```csharp
public class PatientInfo
{
    public string PatientId { get; set; }
    public string PatientName { get; set; }
    public string DateOfBirth { get; set; }
    public string AdministrativeSex { get; set; }
    public string Race { get; set; }
    // ... more fields
}
```

### Message Metadata
```csharp
public class MessageMetadata
{
    public string SendingApplication { get; set; }
    public string MessageType { get; set; }
    public string VersionId { get; set; }
    // ... more fields
}
```

## Segment Reference

### MSH (Message Header)
- `SendingApplication`: Sending system
- `ReceivingFacility`: Receiving system
- `MessageType`: Type of message (e.g., VXU)
- `VersionId`: HL7 version (e.g., 2.5.1)

### PID (Patient Identification)
- `PatientId`: Patient identifier
- `PatientName`: Patient name (XPN format)
- `DateOfBirth`: DOB (YYYYMMDD)
- `AdministrativeSex`: M/F/O/U
- `Race`: Race code
- `PhoneNumberHome`: Home phone
- `PrimaryLanguage`: Language code

### RXA (Vaccine Administration)
- `DateTimeOfAdministration`: Admin date/time
- `AdministeredCode`: Vaccine code
- `AdministeredAmount`: Dose amount
- `AdministeredUnits`: Dose units (mL, etc.)
- `AdministrationSite`: Injection site
- `AdministrationRoute`: Route (IM, SC, etc.)
- `SubstanceLotNumber`: Vaccine lot number
- `SubstanceExpirationDate`: Expiration date
- `CompletionStatus`: Completion status

### OBX (Observation/Result)
- `ValueType`: Type of value (CE, NM, etc.)
- `ObservationIdentifier`: What was observed
- `ObservationValue`: The result value
- `ObservationResultStatus`: Status (F, P, etc.)

## Error Handling

### Common Errors
```csharp
// ArgumentException: Invalid message format
try
{
    var message = parser.ParseMessage(null);
}
catch (ArgumentException ex)
{
    Console.WriteLine("Invalid message");
}

// Check for missing segments
var pid = message.GetSegment<PIDSegment>("PID");
if (pid == null)
    Console.WriteLine("No patient segment found");
```

## Tips & Tricks

1. **Always parse MSH first**: The parser needs to extract separators from MSH
2. **Use CAIR2Parser for vaccination data**: It provides convenient methods for extraction
3. **Validate messages**: Use the validator to ensure compliance
4. **Handle null returns**: `GetSegment()` returns null if not found
5. **Composite fields**: Use `GetComponent()` to access parts of composite values

## Performance Tips

- Use `GetSegment<T>()` for single segment lookup
- Use `GetSegments<T>()` for multiple occurrences
- Parse complete messages once, then query as needed
- Validation can be run after parsing if needed

## Real-World Example

```csharp
try
{
    // Parse
    var cair2 = new CAIR2Parser();
    var message = cair2.ParseVaccinationMessage(hl7Message);
    
    // Validate
    var validator = new Hl7MessageValidator();
    var validResult = validator.Validate(message);
    
    if (!validResult.IsValid)
    {
        Console.WriteLine("Message validation failed:");
        foreach (var error in validResult.Errors)
            Console.WriteLine($"  - {error}");
        return;
    }
    
    // Extract data
    var patientInfo = cair2.ExtractPatientInfo(message);
    var metadata = cair2.ExtractMessageMetadata(message);
    var vaccinations = cair2.ExtractVaccinationRecords(message);
    
    // Process
    Console.WriteLine($"Processing {vaccinations.Count} vaccinations for {patientInfo.PatientName}");
    foreach (var vax in vaccinations)
    {
        Console.WriteLine($"  - {vax.VaccineCode} on {vax.AdministrationDate}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

## Class Hierarchy

```
Segment (Base)
├── MSHSegment
├── PIDSegment
├── RXASegment
├── OBXSegment
└── OBRSegment

Hl7Message (Container)
└── List<Segment>

Hl7Parser (Parser)
├── ParseMessage()
├── ParseSegment()
└── ParseXXXSegment()

Hl7MessageValidator (Validation)
├── Validate(message)
└── ValidateSegment(segment)

Hl7MessageSerializer (Serialization)
├── Serialize(message)
└── SerializeSegment(segment)

CAIR2Parser (CAIR2-specific)
├── ParseVaccinationMessage()
├── ValidateCAIR2Message()
├── ExtractVaccinationRecords()
├── ExtractPatientInfo()
└── ExtractMessageMetadata()
```

## Configuration

Default separators (from `Hl7Separators`):
```csharp
public class Hl7Separators
{
    public char FieldSeparator { get; set; } = '|';              // Field
    public char ComponentSeparator { get; set; } = '^';           // Component
    public char RepetitionSeparator { get; set; } = '~';          // Repetition
    public char EscapeCharacter { get; set; } = '\\';             // Escape
    public char SubComponentSeparator { get; set; } = '&';        // SubComponent
}
```

These are automatically extracted from the MSH segment's encoding characters.

## Testing

Run unit tests to verify installation:
```bash
dotnet test Hl7Test/Hl7Test.csproj
```

Tests include:
- Segment parsing
- Message parsing
- Field extraction
- CAIR2 operations
- Validation
- Serialization
