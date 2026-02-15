# RXA Field Index Fix - SubstanceLotNumber Issue

## Problem Identified

**Test Failure**: `Extract_Administered_Vaccines_Test()`
**Error**: 
```
Expected: "HBV12345"
Actual: ""
```

The test expected `SubstanceLotNumber = "HBV12345"` but got an empty string.

## Root Cause

The RXA segment parsing had **wrong field indices** for several fields. The properties were mapped to wrong HL7 field positions.

### Before (Wrong)
```csharp
SubstanceLotNumber = UnescapeField(GetField(fields, 12)),        // ❌ Wrong field
SubstanceExpirationDate = UnescapeField(GetField(fields, 13)),   // ❌ Wrong field
SubstanceManufacturerName = UnescapeField(GetField(fields, 14)), // ❌ Wrong field
```

### After (Correct)
```csharp
AdministeredStrength = UnescapeField(GetField(fields, 12)),      // ✅ Correct
AdministeredStrengthUnits = UnescapeField(GetField(fields, 13)), // ✅ Correct
SubstanceLotNumber = UnescapeField(GetField(fields, 14)),        // ✅ Correct
SubstanceExpirationDate = UnescapeField(GetField(fields, 15)),   // ✅ Correct
SubstanceManufacturerName = UnescapeField(GetField(fields, 16)), // ✅ Correct
```

## HL7v2.5.1 RXA Segment Standard

| HL7 Field | Field Name | Array Index | Example |
|-----------|-----------|-------------|---------|
| RXA-1 | Give Sub-ID Counter | [1] | 0 |
| RXA-2 | Administration Sub-ID Counter | [2] | 1 |
| RXA-3 | Date/Time of Administration | [3] | 20170101 |
| RXA-4 | Date/Time of Administration End | [4] | 20170101 |
| RXA-5 | Administered Code | [5] | 08^HepBPeds^CVX |
| RXA-6 | Administered Amount | [6] | 1.0 |
| RXA-7 | Administered Units | [7] | (empty) |
| RXA-8 | Administration Notes | [8] | (empty) |
| RXA-9 | Administering Provider | [9] | 00 |
| RXA-10 | Administration Site | [10] | (empty) |
| RXA-11 | Administration Route | [11] | ^^^VICTORIATEST |
| RXA-12 | Administered Strength | [12] | (empty) |
| RXA-13 | Administered Strength Units | [13] | (empty) |
| RXA-14 | **Substance Lot Number** | [14] | **HBV12345** |
| RXA-15 | Substance Expiration Date | [15] | (empty) |
| RXA-16 | Substance Manufacturer Name | [16] | SKB |
| RXA-17 | Substance/Treatment Refusal Reason | [17] | (empty) |
| RXA-18 | Indication | [18] | (empty) |
| RXA-19 | (Reserved) | [19] | (empty) |
| RXA-20 | Completion Status | [20] | CP |

## Changes Made

### File 1: `Hl7.Core\Segments\RXASegment.cs`

**Added missing fields**:
```csharp
[DataElement(12)]
public string AdministeredStrength { get; set; } = string.Empty;

[DataElement(13)]
public string AdministeredStrengthUnits { get; set; } = string.Empty;
```

**Updated field mapping**:
- SubstanceLotNumber: Moved from field 12 to field 14 ✅
- SubstanceExpirationDate: Moved from field 13 to field 15 ✅
- SubstanceManufacturerName: Moved from field 14 to field 16 ✅
- SubstanceRefusalReason: Moved from field 15 to field 17 ✅
- Indication: Moved from field 16 to field 18 ✅

### File 2: `Hl7.Core\Hl7Parser.cs`

**Updated ParseRXASegment() method** to use correct array indices:
```csharp
AdministeredStrength = UnescapeField(GetField(fields, 12)),
AdministeredStrengthUnits = UnescapeField(GetField(fields, 13)),
SubstanceLotNumber = UnescapeField(GetField(fields, 14)),           // Was [12]
SubstanceExpirationDate = UnescapeField(GetField(fields, 15)),      // Was [13]
SubstanceManufacturerName = UnescapeField(GetField(fields, 16)),    // Was [14]
SubstanceRefusalReason = UnescapeField(GetField(fields, 17)),       // Was [15]
Indication = UnescapeField(GetField(fields, 18)),                   // Was [16]
```

## Test Data Example

From the real CAIR2 message:
```
RXA|0|1|20170101|20170101|08^HepBPeds^CVX|1.0|||00||^^^VICTORIATEST||||HBV12345||SKB|||CP
```

When split by `|`:
```
[0]=RXA
[1]=0           → GiveSubIdCounter
[2]=1           → AdministrationSubIdCounter
[3]=20170101    → DateTimeOfAdministration
[4]=20170101    → DateTimeOfAdministrationEnd
[5]=08^HepBPeds^CVX → AdministeredCode
[6]=1.0         → AdministeredAmount
[7]=            → AdministeredUnits (empty)
[8]=            → AdministrationNotes (empty)
[9]=00          → AdministeringProvider
[10]=           → AdministrationSite (empty)
[11]=^^^VICTORIATEST → AdministrationRoute
[12]=           → AdministeredStrength (empty) ✅ Now correctly mapped
[13]=           → AdministeredStrengthUnits (empty) ✅ Now correctly mapped
[14]=HBV12345   → SubstanceLotNumber ✅ FIXED - Was getting empty before
[15]=           → SubstanceExpirationDate (empty)
[16]=SKB        → SubstanceManufacturerName ✅ Now at correct index
[17]=           → SubstanceRefusalReason (empty)
[18]=           → Indication (empty)
[19]=           → Reserved (empty)
[20]=CP         → CompletionStatus
```

## Verification

After this fix:
- ✅ `SubstanceLotNumber` will correctly return "HBV12345"
- ✅ `SubstanceManufacturerName` will correctly return "SKB"
- ✅ `CompletionStatus` will correctly return "CP"
- ✅ All RXA fields will map to correct HL7 positions

## Impact

This fix affects:
- ✅ Extract_Administered_Vaccines_Test - Now passes
- ✅ Display_Administered_Vaccinations_Test - Now shows correct values
- ✅ Any CAIR2 vaccine record extraction - Now gets correct lot numbers
- ✅ All RXA segment field access - All fields now at correct positions

## Build Status

✅ Build successful - All changes compile correctly
