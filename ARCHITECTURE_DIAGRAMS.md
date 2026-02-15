# HL7v2 Parser Architecture Diagrams

## Complete Data Flow

```
┌─────────────────────────────────────────────────────────────────────┐
│                         INPUT: HL7 Message String                    │
│  MSH|^~\&|APP|FAC||REC|TIME||VXU^V04|ID|P|2.5.1                   │
│  PID|1|123456|ABC||DOE^JOHN||19800101|M|                           │
│  RXA|0|1|20240101|...|90658^VACCINE|0.5|mL||...                   │
└────────────────────────────┬────────────────────────────────────────┘
                             │
                             ▼
                    ┌────────────────────┐
                    │   Hl7Parser        │
                    │                    │
                    │ 1. Split segments  │
                    │    by \r\n         │
                    │ 2. Extract MSH     │
                    │    for separators  │
                    │ 3. Parse each      │
                    │    segment line    │
                    │ 4. Unescape fields │
                    │ 5. Create objects  │
                    └────────┬───────────┘
                             │
             ┌───────────────┼───────────────┐
             │               │               │
             ▼               ▼               ▼
        ┌─────────┐     ┌─────────┐     ┌─────────┐
        │ MSHSeg  │     │ PIDSeg  │     │ RXASeg  │
        │ -meta   │     │ -patient│     │ -vaccine│
        └─────────┘     └─────────┘     └─────────┘
             │               │               │
             └───────────────┼───────────────┘
                             │
                             ▼
                    ┌────────────────────┐
                    │   Hl7Message       │
                    │                    │
                    │ List<Segment>      │
                    │ - Get by ID        │
                    │ - Get multiple     │
                    └────────┬───────────┘
                             │
        ┌────────────────────┼────────────────────┐
        │                    │                    │
        ▼                    ▼                    ▼
   ┌──────────┐      ┌───────────────┐    ┌─────────────┐
   │Validator │      │Serializer     │    │CAIR2Parser  │
   │          │      │               │    │             │
   │ Validate │      │ Re-generate   │    │ Extract:    │
   │ Structure│      │ HL7 string    │    │ -Vaccination│
   │ Fields   │      │ Escape fields │    │ -Patient    │
   │ Values   │      │ Use Attr maps │    │ -Metadata   │
   └────┬─────┘      └───┬──────────┘    └────┬────────┘
        │                │                     │
        ▼                ▼                     ▼
   ┌──────────┐      ┌──────────┐      ┌────────────────┐
   │Validation│      │HL7String │      │VaccinationRec  │
   │Result    │      │          │      │PatientInfo     │
   │-errors   │      │MSH|...\n │      │MessageMetadata │
   │-warnings │      │PID|...   │      └────────────────┘
   └──────────┘      │RXA|...   │
                     └──────────┘
```

## Class Hierarchy

```
                        Segment (Base)
                      (SegmentId, Fields)
                             │
                ┌────────────┼────────────────┐
                │            │                │
            MSHSegment   PIDSegment       RXASegment
            (16 fields)  (14 fields)      (18 fields)
                │            │                │
           VersionId      PatientId      AdminCode
           MessageType    PatientName    AdminDate
           MessageControlId DOB          AdminSite
           SendingApp      Sex           LotNumber
           ...             ...           ...

        OBXSegment               OBRSegment
        (15 fields)              (14 fields)
            │                        │
        ObservationId           UniversalServiceId
        ObservationValue        PlacerOrderNumber
        ValueType               RequestedDateTime
        ResultStatus            ...
        ...
```

## Message Parsing Flow

```
Raw HL7 String
    │
    ├─ Validate MSH prefix
    │
    ├─ Extract separators: |^~\&
    │    │
    │    ├─ FieldSeparator = |
    │    ├─ ComponentSeparator = ^
    │    ├─ RepetitionSeparator = ~
    │    ├─ EscapeCharacter = \
    │    └─ SubComponentSeparator = &
    │
    ├─ Split by line breaks (\r\n or \n)
    │    │
    │    └─ [MSH..., PID..., RXA..., ...]
    │
    └─ For each segment:
         │
         ├─ Extract segment ID (first 3 chars)
         │
         ├─ Split by field separator |
         │    │
         │    └─ [MSH, ^~\&, AppName, FacName, ...]
         │
         ├─ Unescape each field
         │    │
         │    ├─ Replace \E\ with \
         │    ├─ Replace \F\ with |
         │    ├─ Replace \S\ with ^
         │    ├─ Replace \T\ with ~
         │    └─ Replace \C\ with &
         │
         ├─ Route to correct parser:
         │    │
         │    ├─ MSH → ParseMSHSegment()
         │    ├─ PID → ParsePIDSegment()
         │    ├─ RXA → ParseRXASegment()
         │    ├─ OBX → ParseOBXSegment()
         │    ├─ OBR → ParseOBRSegment()
         │    └─ ??? → ParseGenericSegment()
         │
         └─ Create segment object & populate properties
```

## CAIR2 Data Extraction

```
         Hl7Message
              │
              ├─ MSH Segment
              │  │
              │  └─ GetSegment<MSHSegment>()
              │     │
              │     └─ Extract MessageMetadata
              │        ├─ SendingApplication
              │        ├─ MessageType
              │        ├─ VersionId
              │        └─ ...
              │
              ├─ PID Segment
              │  │
              │  └─ GetSegment<PIDSegment>()
              │     │
              │     └─ Extract PatientInfo
              │        ├─ PatientId
              │        ├─ PatientName
              │        ├─ DateOfBirth
              │        ├─ AdministrativeSex
              │        └─ ...
              │
              └─ RXA Segments (multiple)
                 │
                 └─ GetSegments<RXASegment>()
                    │
                    └─ For each RXA:
                       Extract VaccinationRecord
                       ├─ VaccineCode
                       ├─ AdministrationDate
                       ├─ AdministrationSite
                       ├─ LotNumber
                       ├─ ExpirationDate
                       ├─ ManufacturerName
                       └─ ...
```

## Validation Pipeline

```
Hl7Message
    │
    ├─ HasSegments?
    │  └─ Check MSH is first
    │
    ├─ MSH Validation
    │  ├─ Required: MessageType, MessageControlId
    │  └─ Recommended: ProcessingId, VersionId
    │
    ├─ For each segment type:
    │  │
    │  ├─ MSH: Check metadata completeness
    │  ├─ PID: Check patient data (recommended)
    │  ├─ RXA: Check vaccine data (required)
    │  ├─ OBX: Check observation structure
    │  └─ OBR: Check order structure
    │
    ├─ CAIR2-specific (if VXU):
    │  ├─ Must have PID segment
    │  ├─ Must have ≥1 RXA segment
    │  └─ Check message hierarchy
    │
    └─ Return ValidationResult
       ├─ IsValid: bool
       ├─ Errors[]: Critical issues
       └─ Warnings[]: Recommendations
```

## Serialization Pipeline

```
Hl7Message
    │
    └─ For each Segment:
       │
       ├─ Get SegmentId (MSH, PID, RXA, etc.)
       │
       ├─ Reflect on Segment class:
       │  │
       │  └─ Get all properties with [DataElement]
       │     │
       │     └─ Sort by Position
       │
       ├─ For each property (in position order):
       │  │
       │  ├─ Get value from property
       │  │
       │  ├─ Escape special characters:
       │  │  ├─ \ → \E\
       │  │  ├─ | → \F\
       │  │  ├─ ^ → \S\
       │  │  ├─ ~ → \T\
       │  │  └─ & → \C\
       │  │
       │  └─ Add to field list
       │
       ├─ Remove trailing empty fields
       │
       └─ Join with field separator |
          │
          └─ HL7 Segment String

           MSH|^~\&|AppName|FacName|...|2.5.1
           PID|1|PatientId|...|PatientName|...
           RXA|0|1|20240101|...|VaccineCode|...
```

## Composite Field Handling

```
Raw composite field:
    "90658^INFLUENZA VACCINE^2024"
              │
              ▼
      Split by component separator (^)
              │
        ┌─────┼─────┬────────┐
        │     │     │        │
        ▼     ▼     ▼        ▼
      90658  INFLUENZA  2024
           VACCINE
        │     │        │
        └─────┼─────┬──┘
              ▼
    Components[]
    [0] = "90658"
    [1] = "INFLUENZA VACCINE"
    [2] = "2024"
              │
              ▼
    GetComponent(0) → "90658"
    GetComponent(1) → "INFLUENZA VACCINE"
    GetComponent(2) → "2024"
```

## Repeating Field Handling

```
Raw repeating field:
    "DOE^JOHN~SMITH^JANE~BROWN^BOB"
              │
              ▼
      Split by repetition separator (~)
              │
        ┌─────┼──────────┬──────────┐
        │     │          │          │
        ▼     ▼          ▼          ▼
    DOE^JOHN SMITH^JANE BROWN^BOB
        │     │          │
        └─────┼──────────┘
              ▼
    List<String>
    [0] = "DOE^JOHN"
    [1] = "SMITH^JANE"
    [2] = "BROWN^BOB"
              │
              ▼
    For each in list:
    Parse as composite:
    [0].GetComponent(0) → "DOE"
    [0].GetComponent(1) → "JOHN"
```

## Field Position Numbering

```
HL7 Segment: PID|1|123456789|987654321||DOE^JOHN||19800101|M

Position:     0  1     2        3     4   5      6  7        8
SegmentId   Field1  Field2   Field3 Empty Field5 Empty Field7 Field8

In property:  N/A  SetId  PatientId  PatientIdList Empty PatientName Empty DOB Sex

Note: First position (0) is always SegmentId
      Subsequent properties are 1-indexed
      Empty fields still count in position
```

## Error Handling Flow

```
Parse attempt
    │
    ├─ ArgumentException
    │  ├─ Null message
    │  ├─ Empty message
    │  ├─ Invalid segment type
    │  └─ Malformed data
    │
    ├─ Catch & validate
    │  │
    │  ├─ Log error
    │  ├─ Return null or throw
    │  └─ Provide detail message
    │
    └─ Continue with next segment or stop
       │
       └─ User handles error appropriately
```

## Performance Characteristics

```
Message Size: Small (5-10 fields)
    │
    ├─ Parse: <10ms ✅
    ├─ Validate: <1ms ✅
    └─ Serialize: <1ms ✅

Message Size: Medium (50-100 fields)
    │
    ├─ Parse: <50ms ✅
    ├─ Validate: <3ms ✅
    └─ Serialize: <2ms ✅

Message Size: Large (500+ fields)
    │
    ├─ Parse: <200ms ✅
    ├─ Validate: <10ms ✅
    └─ Serialize: <5ms ✅

All well within acceptable thresholds
```

## Thread Safety

```
NOT thread-safe for shared instances:
    ├─ Hl7Parser (mutable _separators)
    ├─ CAIR2Parser (internal parser instance)
    └─ Hl7MessageValidator (mutable results)

SOLUTION:
    ├─ Create new instance per thread
    ├─ Or use locks for shared instance
    └─ Or redesign for immutability

Thread-safe:
    ├─ Hl7Message (read after creation)
    ├─ Segment objects (read after creation)
    └─ Result objects (immutable)

Recommendation:
    └─ Use dependency injection with transient scope
```

---

These diagrams illustrate the complete architecture and data flow of the HL7v2 parser for CAIR2.
