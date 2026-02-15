# ✅ PID Field Index Fix - Extract_Patient_From_Real_Message_Test

## The Problem

Test: `Extract_Patient_From_Real_Message_Test()`
Failed assertion: `PatientAddress`

Expected: `"2222 ANYWHERE WAY^^FRESNO^CA^93726^^H"`
Actual: (empty or wrong value)

## Root Cause

PID segment field indices were **off by one** for PatientAddress and subsequent fields.

### The PID Line from Test Data:
```
PID|1||291235^^^ORA^SR||WALL^MIKE|WINDOW^DOLLY|20170101|M|||2222 ANYWHERE WAY^^FRESNO^CA^93726^^H||^PRN^H^^^555^7575382|||||||||||N|0
```

### Array Positions:
```
[0]  = PID
[1]  = 1                                     (SetId)
[2]  = (empty)
[3]  = 291235^^^ORA^SR                       (PatientIdentifierList)
[4]  = (empty)
[5]  = WALL^MIKE                             (PatientName)
[6]  = WINDOW^DOLLY                          (MothersMaidenName)
[7]  = 20170101                              (DateOfBirth)
[8]  = M                                     (AdministrativeSex)
[9]  = (empty)                               (Race)
[10] = (empty)                               ← Extra empty field!
[11] = 2222 ANYWHERE WAY^^FRESNO^CA^93726^^H (PatientAddress) ← SHOULD BE HERE
[12] = (empty)                               (CountyCode)
[13] = ^PRN^H^^^555^7575382                  (PhoneNumberHome)
```

## The Fix

**File**: `Hl7.Core\Hl7Parser.cs`
**Method**: `ParsePIDSegment()`

### Before (Wrong):
```csharp
PatientAddress = UnescapeField(GetField(fields, 10)),        // ❌ Gets empty
CountyCode = UnescapeField(GetField(fields, 11)),            // ❌ Gets empty
PhoneNumberHome = UnescapeField(GetField(fields, 12)),       // ❌ Gets empty
PhoneNumberBusiness = UnescapeField(GetField(fields, 13))    // ❌ Gets wrong value
```

### After (Correct):
```csharp
PatientAddress = UnescapeField(GetField(fields, 11)),        // ✅ Gets "2222 ANYWHERE WAY^^FRESNO^CA^93726^^H"
CountyCode = UnescapeField(GetField(fields, 12)),            // ✅ Correct
PhoneNumberHome = UnescapeField(GetField(fields, 13)),       // ✅ Gets "^PRN^H^^^555^7575382"
PhoneNumberBusiness = UnescapeField(GetField(fields, 14))    // ✅ Correct
```

## Why This Happens

CAIR2 PID segment has an extra empty field at position [10], which shifts all patient address and phone fields by 1 position from the standard HL7 field numbering.

## Files Changed

1. **Hl7.Core\Hl7Parser.cs**
   - Updated `ParsePIDSegment()` method
   - Shifted PatientAddress from [10] to [11]
   - Shifted all subsequent fields by 1

## New Test Files Created

1. **PIDFieldDiagnostic.cs** - Shows the exact field positions
2. **PIDFieldFixVerification.cs** - Verifies the fix works

## Verification

Run the originally failing test:
```bash
dotnet test Hl7Test/Hl7Test.csproj -k "Extract_Patient_From_Real_Message_Test" -v detailed
```

Or run the verification:
```bash
dotnet test Hl7Test/Hl7Test.csproj -k "PIDFieldFixVerification" -v detailed
```

**Expected**: ✅ All tests PASS

## Build Status

✅ **Build successful** - All changes compile

## Summary of Changes

| Field | Old Index | New Index | Value |
|-------|-----------|-----------|-------|
| PatientAddress | [10] | [11] | 2222 ANYWHERE WAY^^FRESNO^CA^93726^^H |
| CountyCode | [11] | [12] | (empty) |
| PhoneNumberHome | [12] | [13] | ^PRN^H^^^555^7575382 |
| PhoneNumberBusiness | [13] | [14] | (empty) |

---

**Status**: ✅ FIXED and Ready for Testing
