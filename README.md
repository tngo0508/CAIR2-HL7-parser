# HL7v2 Parser for CAIR2

A comprehensive .NET HL7v2 (Health Level 7 version 2) message parser specifically designed for California's Immunization Registry (CAIR2) system.

## Features

- **Full HL7v2 Parsing Support**: Parse complete HL7 messages and individual segments
- **CAIR2-Specific**: Optimized for vaccination/immunization data exchange
- **Segment Support**:
  - **MSH** (Message Header): Message metadata and separators
  - **PID** (Patient Identification): Patient demographic information
  - **PD1** (Patient Additional Demographics)
  - **NK1** (Next of Kin)
  - **RXA** (Pharmacy/Vaccine Administration): Vaccination administration details
  - **RXR** (Pharmacy/Treatment Route)
  - **OBX** (Observation/Result): Lab results and observations
  - **OBR** (Observation Request): Order information
  - **ORC** (Common Order)
  - **QPD** (Query Parameter Definition)
  - **QAK** (Query Acknowledgment)
  - **RCP** (Response Control Parameters)
  - **MSA** (Message Acknowledgment)
  - **ERR** (Error)
  - Generic segment support for other segment types

- **Advanced Features**:
  - Automatic separator extraction from MSH segment
  - Field escaping/unescaping support
  - Composite field parsing
  - Repeating field support
  - Message validation
  - Message serialization
  - Reflection-based segment mapping using attributes

## Installation

Add the Hl7.Core project to your solution and reference it in your project file.

## Basic Usage

### Parsing an HL7 Message

```csharp
using Hl7.Core;
using Hl7.Core.Segments;

var parser = new Hl7Parser();

var hl7Message = @"MSH|^~\&|MyEMR|DE-000001||CAIR2|201607011230300700||VXU^V04^VXU_V04|CA0001|P|2.5.1
PID|1|123456789|987654321||DOE^JOHN||19800101|M|
RXA|0|1|20160701120000||90658^INFLUENZA VACCINE|0.5|mL||00|^LEFT ARM|IM|ABC123|20180630||MERCK||||||A";

// Parse the complete message
var message = parser.ParseMessage(hl7Message);

// Get specific segments
var mshSegment = message.GetSegment<MSHSegment>("MSH");
var pidSegment = message.GetSegment<PIDSegment>("PID");
var rxaSegments = message.GetSegments<RXASegment>("RXA");

// Access segment data
Console.WriteLine($"Patient: {pidSegment.PatientName}");
Console.WriteLine($"DOB: {pidSegment.DateOfBirth}");
Console.WriteLine($"Version: {mshSegment.VersionId}");

// Iterate through vaccination records
foreach (var rxa in rxaSegments)
{
    Console.WriteLine($"Vaccine: {rxa.AdministeredCode}");
    Console.WriteLine($"Date: {rxa.DateTimeOfAdministration}");
    Console.WriteLine($"Administered At: {rxa.AdministeredAtLocation}");
}
```

### Using CAIR2-Specific Parser

```csharp
using Hl7.Core.CAIR2;

var cair2Parser = new CAIR2Parser();

// Parse vaccination message
var message = cair2Parser.ParseVaccinationMessage(hl7Message);

// Validate CAIR2 message structure
if (cair2Parser.ValidateCAIR2Message(message))
{
    // Extract structured vaccination records
    var vaccinations = cair2Parser.ExtractVaccinationRecords(message);
    var patientInfo = cair2Parser.ExtractPatientInfo(message);
    var metadata = cair2Parser.ExtractMessageMetadata(message);
    
    foreach (var record in vaccinations)
    {
        Console.WriteLine($"Patient {record.PatientId} received {record.VaccineCode}");
        Console.WriteLine($"Date: {record.AdministrationDate}");
        Console.WriteLine($"Lot: {record.LotNumber}");
    }
}
```

### CAIR2 Bidirectional (QBP/RSP)

```csharp
using Hl7.Core.CAIR2;

var exchange = new Cair2BidirectionalExchange();

var query = exchange.BuildQbpMessage(
    Cair2QueryProfile.Z44,
    new Cair2QueryParameters
    {
        SendingApplication = "MYAPP",
        SendingFacility = "MYSENDER",
        ReceivingApplication = "CAIR IIS",
        ReceivingFacility = "DE000001",
        MessageControlId = "MSG123",
        QueryTag = "Q0001",
        PatientName = "DOE^JANE",
        DateOfBirth = "20190101",
        AdministrativeSex = "F",
        PatientAddress = "123 MAIN ST^^SACRAMENTO^CA^95814"
    });

// Serialize with Hl7MessageSerializer

// Parse an RSP response
var response = exchange.ParseRspMessage(hl7ResponseString);
Console.WriteLine(response.QueryResponseStatus);
```

### Parsing Individual Segments

```csharp
var parser = new Hl7Parser();

// Parse MSH first to extract separators (required)
var mshSegment = parser.ParseMSHSegment(
    "MSH|^~\\&|MyEMR|DE-000001||CAIR2|201607011230300700||VXU^V04^VXU_V04|CA0001|P|2.5.1");

// Now parse other segments
var pidSegment = parser.ParseSegment("PID|1|123456789|987654321||DOE^JOHN||19800101|M|") as PIDSegment;
var rxaSegment = parser.ParseSegment("RXA|0|1|20160701120000||90658^INFLUENZA VACCINE|0.5|mL||00|^LEFT ARM|IM|ABC123|20180630||MERCK||||||A") as RXASegment;
```

### Working with Composite Fields

```csharp
using Hl7.Core.Utils;

var separators = new Hl7Separators();

// Parse composite field (e.g., "90658^INFLUENZA VACCINE")
var compositeValue = "90658^INFLUENZA VACCINE";
var composite = Hl7FieldHelper.ParseComposite(compositeValue, separators);

Console.WriteLine($"Code: {composite.GetComponent(0)}");      // 90658
Console.WriteLine($"Description: {composite.GetComponent(1)}"); // INFLUENZA VACCINE

// Parse repeating fields
var repeatingValue = "DOE^JOHN~SMITH^JANE";
var repeatingFields = Hl7FieldHelper.ParseRepeating(repeatingValue, separators);
```

### Message Validation

```csharp
using Hl7.Core.Validation;

var validator = new Hl7MessageValidator();
var result = validator.Validate(message);

if (result.IsValid)
{
    Console.WriteLine("Message is valid!");
}
else
{
    foreach (var error in result.Errors)
        Console.WriteLine($"Error: {error}");
    
    foreach (var warning in result.Warnings)
        Console.WriteLine($"Warning: {warning}");
}

// Validate individual segments
var pidResult = validator.ValidateSegment(pidSegment);
var rxaResult = validator.ValidateSegment(rxaSegment);
```

### Message Serialization

```csharp
using Hl7.Core;
using Hl7.Core.Utils;

// Create segments
var msh = new MSHSegment
{
    SendingApplication = "MyEMR",
    ReceivingFacility = "CAIR2",
    VersionId = "2.5.1",
    MessageType = "VXU^V04^VXU_V04",
    MessageControlId = "MSG0001"
};

var pid = new PIDSegment
{
    SetId = 1,
    PatientId = "123456789",
    PatientName = "DOE^JOHN",
    DateOfBirth = "19800101"
};

// Create message
var message = new Hl7Message();
message.AddSegment(msh);
message.AddSegment(pid);

// Serialize to HL7 format
var separators = new Hl7Separators();
var serializer = new Hl7MessageSerializer(separators);
var hl7String = serializer.Serialize(message);

Console.WriteLine(hl7String);
```

## HL7v2 Message Structure

### Separators

HL7v2 uses special characters to delimit data:

| Character | Name | Default | Purpose |
|-----------|------|---------|---------|
| &#124; | Field Separator | &#124; | Separates fields within a segment |
| ^ | Component Separator | ^ | Separates components within a field |
| ~ | Repetition Separator | ~ | Separates repeated fields |
| \\ | Escape Character | \\ | Escapes special characters |
| & | Sub-Component Separator | & | Separates sub-components |

The MSH segment defines these separators:
```
MSH|^~\&|...
    ||||
    ||||---- Escape Character (\)
    |||------ Repetition Separator (~)
    ||------- Component Separator (^)
    |-------- Field Separator (|)
```

### Segment Field References

#### MSH - Message Header
```
MSH-1:  Field Separator
MSH-2:  Encoding Characters (^~\&)
MSH-3:  Sending Application
MSH-4:  Sending Facility
MSH-5:  Receiving Application
MSH-6:  Receiving Facility
MSH-7:  Date/Time of Message
MSH-9:  Message Type
MSH-10: Message Control ID
MSH-11: Processing ID
MSH-12: Version ID
```

#### PID - Patient Identification
```
PID-1:  Set ID
PID-2:  Patient ID
PID-3:  Patient Identifier List
PID-5:  Patient Name (XPN)
PID-7:  Date of Birth
PID-8:  Administrative Sex (M/F/O/U)
PID-10: Patient Address
PID-13: Phone Number - Home
PID-15: Primary Language
```

#### RXA - Pharmacy/Vaccine Administration
```
RXA-1:  Give Sub-ID Counter
RXA-3:  Date/Time of Administration
RXA-5:  Administered Code
RXA-6:  Administered Amount
RXA-10: Administering Provider
RXA-11: Administered-at Location
RXA-15: Substance Lot Number
RXA-16: Substance Expiration Date
RXA-17: Substance Manufacturer
```

#### OBX - Observation/Result
```
OBX-1:  Set ID
OBX-2:  Value Type
OBX-3:  Observation Identifier
OBX-5:  Observation Value
OBX-11: Observation Result Status
```

#### OBR - Observation Request
```
OBR-1:  Set ID
OBR-4:  Universal Service Identifier
OBR-6:  Requested Date/Time
OBR-7:  Observation Date/Time
```

## Architecture

### Core Classes

- **Hl7Parser**: Main parser class for parsing HL7 messages and segments
- **Hl7Message**: Container for parsed segments with helper methods for retrieval
- **Segment**: Base class for all segment types
- **Hl7Separators**: Manages HL7 separator characters
- **Hl7FieldHelper**: Utilities for field parsing and escaping
- **Hl7MessageValidator**: Validates message compliance with HL7v2 standards
- **Hl7MessageSerializer**: Converts segments back to HL7 format
- **CAIR2Parser**: CAIR2-specific functionality and data extraction

### Attributes

- **SegmentAttribute**: Marks segment classes with their HL7 ID
- **DataElementAttribute**: Maps properties to field positions

## Error Handling

All parser methods include validation and will throw appropriate exceptions:

```csharp
try
{
    var message = parser.ParseMessage(hl7String);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Parsing error: {ex.Message}");
}
```

## Performance Considerations

- The parser uses efficient string splitting and indexing
- Reflection for attribute-based mapping is done per-class, not per-instance
- Large messages with hundreds of segments parse efficiently
- Use `ParseSegment` for individual segment parsing to avoid full message parsing

## CAIR2 Specific Information

This parser is optimized for California's Immunization Registry (CAIR2) VXU messages:

- **VXU (Vaccination Update)**: Message type for immunization data
- **Supports multiple vaccinations per message**: Multiple RXA segments
- **Composite field handling**: Vaccine codes use component structure (code^description)
- **CAIR2 validation**: Checks for required segments and fields per CAIR2 specifications

## Unit Tests

Comprehensive unit tests are included covering:

- MSH segment parsing
- PID segment parsing
- RXA segment parsing
- OBX segment parsing
- Complete message parsing
- Message validation
- CAIR2-specific extraction
- Field parsing and escaping
- Message serialization

Run tests with:
```bash
dotnet test Hl7Test/Hl7Test.csproj
```

## Dependencies

- .NET 10
- C# 14.0
- No external dependencies for core parsing

## License

Designed for CAIR2 immunization data exchange.

## Contributing

When adding new segment types:

1. Create a new class inheriting from `Segment`
2. Add properties with `DataElementAttribute` for field mapping
3. Add `SegmentAttribute` with the segment ID
4. Initialize `SegmentId` in the constructor
5. Add parsing logic to `Hl7Parser.ParseSegment()`
6. Add validation logic if needed to `Hl7MessageValidator`
7. Add unit tests for the new segment type

## References

- HL7 Standard: https://www.hl7.org/
- HL7v2.5.1 Specification
- CAIR2 Implementation Guide
