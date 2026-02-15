# Test Troubleshooting Guide

## If Tests Are Still Failing

This guide helps identify and fix remaining issues.

### Step 1: Run Minimal Tests First

Run `MinimalMSHTest.cs` to verify basic parsing works:

```bash
dotnet test Hl7Test/Hl7Test.csproj -k "MinimalMSHTest"
```

Expected output:
- ✅ Verify_MSH_Fields_Are_Extracted_Correctly - PASS
- ✅ Verify_Full_Message_Parsing - PASS

If these fail, the issue is in the parser itself.

### Step 2: Run Diagnostic Tests

Run `DiagnosticParsingTests.cs` to see detailed field extraction:

```bash
dotnet test Hl7Test/Hl7Test.csproj -k "DiagnosticParsingTests"
```

This will print out:
- What values were extracted
- What values were expected
- The field array contents

Use this output to identify which fields are wrong.

### Step 3: Check Specific Test Categories

#### MSH Tests
```bash
dotnet test Hl7Test/Hl7Test.csproj -k "Extract_MSH"
```

Should pass if field indices are correct.

#### PID Tests
```bash
dotnet test Hl7Test/Hl7Test.csproj -k "Extract_Patient"
```

Should pass if PID parsing works.

#### RXA Tests
```bash
dotnet test Hl7Test/Hl7Test.csproj -k "Extract_Administered"
```

Should pass if RXA parsing works.

### Step 4: Common Issues and Solutions

#### Issue: SendingApplication returns encoding characters

**Symptom**: `SendingApplication == "^~\&"` instead of `"CAIR IIS"`

**Cause**: MSH field indices are wrong

**Solution**: Verify indices in `ParseMSHSegment()`:
- SendingApplication should use index 2 (not 1)
- SendingFacility should use index 3 (not 2)
- All indices shifted by +1 from HL7 field numbers

#### Issue: PID fields are empty

**Symptom**: `PatientName == ""` instead of `"WALL^MIKE"`

**Cause**: PID field indices are wrong OR separators weren't parsed

**Solution**: 
1. Make sure MSH is parsed FIRST
2. Verify PID indices start from 1 (after segment ID)
3. Check that GetField handles boundaries correctly

#### Issue: Message has 0 segments

**Symptom**: `message.Segments.Count == 0`

**Cause**: ParseMessage isn't splitting lines correctly

**Solution**: Check that line splitting handles both \r\n and \n

### Step 5: Verify Separators

The separators should be:
- FieldSeparator: `|`
- ComponentSeparator: `^`
- RepetitionSeparator: `~`
- EscapeCharacter: `\`
- SubComponentSeparator: `&`

If these are wrong, all field extraction will fail.

### Step 6: Manual Field Count Verification

For MSH line:
```
MSH|^~\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1|...
```

Split by `|` should give:
```
[0]  = "MSH"
[1]  = "^~\&"
[2]  = "CAIR IIS"          <- SendingApplication
[3]  = "CAIR IIS"          <- SendingFacility
[4]  = ""                   <- ReceivingApplication
[5]  = "DE000001"          <- ReceivingFacility
[6]  = "20170509"          <- MessageDateTime
[7]  = ""                   <- Security
[8]  = "RSP^K11^RSP_K11"   <- MessageType
[9]  = "200"                <- MessageControlId
[10] = "P"                  <- ProcessingId
[11] = "2.5.1"             <- VersionId
```

For PID line:
```
PID|1||291235^^^ORA^SR||WALL^MIKE|WINDOW^DOLLY|20170101|M|...
```

Split by `|` should give:
```
[0] = "PID"
[1] = "1"                   <- SetId
[2] = ""                    <- PatientId
[3] = "291235^^^ORA^SR"    <- PatientIdentifierList
[4] = ""                    <- AlternatePatientId
[5] = "WALL^MIKE"          <- PatientName
[6] = "WINDOW^DOLLY"       <- MothersMaidenName
[7] = "20170101"           <- DateOfBirth
[8] = "M"                   <- AdministrativeSex
```

### Step 7: Check for String Escaping Issues

HL7 uses backslash escaping. Check if:
- `\\` in strings is being handled correctly
- Escaping functions are working properly
- UnescapeField is being called at the right time

### Step 8: Verify Message Structure

A valid message should have:
- 1 MSH segment (required first)
- 1 or more other segments

If ParseMessage returns 0 segments, the line splitting is broken.

---

## How to Report Issues

When reporting test failures, include:

1. **Test Name**: Which test is failing?
2. **Expected Value**: What should it be?
3. **Actual Value**: What did it return?
4. **Diagnostic Output**: Run diagnostic tests and show output
5. **Code Context**: Which parsing method is involved?

---

## Verification Checklist

Before declaring success, verify:

- [ ] MinimalMSHTest passes (2/2)
- [ ] MSH field extraction correct
- [ ] PID field extraction correct
- [ ] RXA field extraction correct
- [ ] OBX field extraction correct
- [ ] Message contains all segments
- [ ] Separators are set correctly
- [ ] No empty strings where data expected
- [ ] All 29 tests in RealWorldCAIR2Tests pass

---

## Success Criteria

✅ All tests pass
✅ MSH fields extract correctly
✅ Patient data readable
✅ Vaccine records accessible
✅ Forecast data parseable
✅ No errors or exceptions
✅ Real-world message validates
