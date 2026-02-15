# RXA Field Position Analysis - CORRECT SOLUTION

## Test Data Analysis

RXA Line:
```
RXA|0|1|20170101|20170101|08^HepBPeds^CVX|1.0|||00||^^^VICTORIATEST||||HBV12345||SKB|||CP
```

## Exact Field Breakdown

```
Pos  Value                   Field
[0]  RXA                     Segment ID
[1]  0                       RXA-1: Give Sub-ID Counter
[2]  1                       RXA-2: Administration Sub-ID Counter
[3]  20170101                RXA-3: Date/Time of Administration
[4]  20170101                RXA-4: Date/Time of Administration End
[5]  08^HepBPeds^CVX        RXA-5: Administered Code
[6]  1.0                     RXA-6: Administered Amount
[7]  (empty)                 RXA-7: Administered Units
[8]  (empty)                 RXA-8: Administration Notes
[9]  00                      RXA-9: Administering Provider
[10] (empty)                 RXA-10: Administration Site
[11] ^^^VICTORIATEST         RXA-11: Administration Route
[12] (empty)                 RXA-12: Administered Strength
[13] (empty)                 RXA-13: Administered Strength Units
[14] (empty)                 RXA-14: (empty field)
[15] HBV12345                RXA-15: Substance Lot Number ← KEY POSITION
[16] (empty)                 RXA-16: Substance Expiration Date
[17] SKB                     RXA-17: Substance Manufacturer Name ← KEY POSITION
[18] (empty)                 RXA-18: (empty)
[19] (empty)                 RXA-19: (empty)
[20] CP                      RXA-20: Completion Status ← KEY POSITION
```

## Critical Finding

**HBV12345 is at array position [15], NOT [14]**
**SKB is at array position [17], NOT [16]**

This means the mapping should be:
- SubstanceLotNumber = fields[15]
- SubstanceManufacturerName = fields[17]
- CompletionStatus = fields[20]

## Current Wrong Implementation

My previous fix used:
```csharp
SubstanceLotNumber = GetField(fields, 14),           // ❌ WRONG
SubstanceManufacturerName = GetField(fields, 16),    // ❌ WRONG
CompletionStatus = GetField(fields, 20),             // ✅ CORRECT
```

## Correct Implementation Needed

```csharp
SubstanceLotNumber = GetField(fields, 15),           // ✅ CORRECT
SubstanceExpirationDate = GetField(fields, 16),      // ✅ CORRECT
SubstanceManufacturerName = GetField(fields, 17),    // ✅ CORRECT
SubstanceRefusalReason = GetField(fields, 18),       // (empty but correct)
Indication = GetField(fields, 19),                   // (empty but correct)
CompletionStatus = GetField(fields, 20),             // ✅ CORRECT
```

## CAIR2 RXA Structure

The CAIR2 RXA segment appears to have:
- Standard fields 1-14 (as per HL7)
- An extra empty field at position 14
- Lot Number at position 15 (shifted by 1 from HL7 standard)
- Manufacturer Name at position 17 (shifted by 1 from HL7 standard)

This suggests CAIR2 adds an extra field OR has a different structure than standard HL7v2.5.1.
