# How to Identify Exactly Which Tests Are Failing

## Run Each Test Individually

### Step 1: Run the Ultra Simple Tests First

These show EXACTLY what values are being extracted:

```bash
dotnet test Hl7Test/Hl7Test.csproj -k "UltraSimpleTest" -v detailed
```

**Expected Output** includes:
```
=== EXACT FIELD VALUES ===
[0]: 'MSH'
[1]: '^~\'
[2]: 'CAIR IIS'
[3]: 'CAIR IIS'
...

=== PARSER OUTPUT ===
SendingApplication: 'CAIR IIS'
SendingFacility: 'CAIR IIS'
...
```

If this passes ✅, the parser is working
If this fails ❌, the parser has issues

---

### Step 2: Run RealWorldCAIR2Tests One at a Time

```bash
# Run each test individually to see which fails
dotnet test Hl7Test/Hl7Test.csproj -k "Parse_Real_CAIR2_RSP_Message_Test" -v detailed
dotnet test Hl7Test/Hl7Test.csproj -k "Extract_Patient_From_Real_Message_Test" -v detailed
dotnet test Hl7Test/Hl7Test.csproj -k "Extract_MSH_From_Real_Message_Test" -v detailed
dotnet test Hl7Test/Hl7Test.csproj -k "Count_RXA_Segments_Test" -v detailed
dotnet test Hl7Test/Hl7Test.csproj -k "Extract_Administered_Vaccines_Test" -v detailed
dotnet test Hl7Test/Hl7Test.csproj -k "Count_OBX_Segments_Test" -v detailed
dotnet test Hl7Test/Hl7Test.csproj -k "Extract_First_OBX_Observation_Test" -v detailed
dotnet test Hl7Test/Hl7Test.csproj -k "Validate_Real_Message_Structure_Test" -v detailed
dotnet test Hl7Test/Hl7Test.csproj -k "Extract_Patient_Demographics_Test" -v detailed
dotnet test Hl7Test/Hl7Test.csproj -k "Display_Administered_Vaccinations_Test" -v detailed
dotnet test Hl7Test/Hl7Test.csproj -k "Parse_Vaccine_Forecast_Test" -v detailed
dotnet test Hl7Test/Hl7Test.csproj -k "Message_Has_Correct_Segment_Count_Test" -v detailed
dotnet test Hl7Test/Hl7Test.csproj -k "Serialize_And_Reparse_Test" -v detailed
dotnet test Hl7Test/Hl7Test.csproj -k "Extract_Message_Metadata_Test" -v detailed
dotnet test Hl7Test/Hl7Test.csproj -k "Message_Contains_All_Required_Elements_Test" -v detailed
```

---

## Failure Analysis

### If UltraSimpleTest Fails

The parser itself has issues. Look at:
- What values are actually being extracted?
- Are they shifted? Off by one?
- Are they empty?
- Do they have encoding characters?

### If UltraSimpleTest Passes but RealWorldCAIR2Tests Fails

The issue is not in basic parsing, but in:
- Full message parsing (multiple segments)
- Segment type mapping
- GetSegment<T>() lookup
- Field type conversions

### Failure Message Format

When a test fails, you'll see something like:

```
Expected: "CAIR IIS"
Actual:   "^~\&"
```

This tells you:
- **Expected**: What should be there
- **Actual**: What you got instead
- **Pattern**: All off by one? Missing data? Wrong field?

---

## Copy-Paste These Commands

### Check 1: Is basic parsing working?
```bash
dotnet test Hl7Test/Hl7Test.csproj -k "UltraSimpleTest" --logger "console;verbosity=normal"
```

### Check 2: What are actual vs expected values?
```bash
dotnet test Hl7Test/Hl7Test.csproj -k "Debug_MSH_Field_Extraction" --logger "console;verbosity=detailed" 2>&1 | tee test_output.txt
```

### Check 3: List all failing tests
```bash
dotnet test Hl7Test/Hl7Test.csproj --logger "console;verbosity=normal" 2>&1 | grep "FAILED"
```

---

## Report Template

When reporting failures, please provide:

**Test that failed**: [name of test]
**Expected value**: [what should it be]
**Actual value**: [what did it return]
**Type**: [string, int, list, etc.]

Example:
```
Test that failed: Extract_MSH_From_Real_Message_Test
Expected value: "CAIR IIS"
Actual value: "^~\&"
Type: string
```

---

## Next Actions

1. **Run UltraSimpleTest** - determines if parser is broken
2. **Look at output** - shows actual extracted values
3. **Compare** - actual vs expected
4. **Identify pattern** - all wrong? Some wrong? Off by one?
5. **Report back** - with specific values and which tests fail

This will give us exactly what we need to fix it.
