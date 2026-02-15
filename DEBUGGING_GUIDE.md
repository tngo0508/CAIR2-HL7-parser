# Step-by-Step Debugging Guide

## Quick Debug: Check Core Parsing

### 1. Verify MSH Parsing

Create a simple test:
```csharp
var parser = new Hl7Parser();
var msh = parser.ParseMSHSegment("MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1");

Console.WriteLine($"SendingApplication: {msh.SendingApplication}");    // Should be "CAIR IIS"
Console.WriteLine($"SendingFacility: {msh.SendingFacility}");          // Should be "CAIR IIS"
Console.WriteLine($"ReceivingFacility: {msh.ReceivingFacility}");      // Should be "DE000001"
Console.WriteLine($"VersionId: {msh.VersionId}");                      // Should be "2.5.1"
```

### 2. Verify Separator Extraction

```csharp
var separators = Hl7Separators.ParseFromMSH("MSH|^~\\&|test|");

Console.WriteLine($"FieldSeparator: '{separators.FieldSeparator}'");        // Should be '|'
Console.WriteLine($"ComponentSeparator: '{separators.ComponentSeparator}'"); // Should be '^'
Console.WriteLine($"RepetitionSeparator: '{separators.RepetitionSeparator}"); // Should be '~'
Console.WriteLine($"EscapeCharacter: '{separators.EscapeCharacter}'");      // Should be '\'
Console.WriteLine($"SubComponentSeparator: '{separators.SubComponentSeparator}"); // Should be '&'
```

### 3. Verify Field Splitting

```csharp
var mshLine = "MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1";
var fields = mshLine.Split('|');

for (int i = 0; i < fields.Length && i < 12; i++)
{
    Console.WriteLine($"fields[{i}] = '{fields[i]}'");
}

// Expected output:
// fields[0] = 'MSH'
// fields[1] = '^~\'
// fields[2] = 'CAIR IIS'
// fields[3] = 'CAIR IIS'
// fields[4] = ''
// fields[5] = 'DE000001'
// fields[6] = '20170509'
// fields[7] = ''
// fields[8] = 'RSP^K11^RSP_K11'
// fields[9] = '200'
// fields[10] = 'P'
// fields[11] = '2.5.1'
```

### 4. Check GetField Implementation

```csharp
public string GetField(string[] fields, int index)
{
    return index >= 0 && index < fields.Length ? fields[index] : string.Empty;
}

// Test it:
var fields = "MSH|^~\\&|CAIR IIS".Split('|');
Console.WriteLine(GetField(fields, 2));  // Should be "CAIR IIS"
Console.WriteLine(GetField(fields, 99)); // Should be ""
```

### 5. Test ParseInt Helper

```csharp
private int ParseInt(string value)
{
    return int.TryParse(value, out var result) ? result : 0;
}

// Test it:
Console.WriteLine(ParseInt("1"));     // Should be 1
Console.WriteLine(ParseInt(""));      // Should be 0
Console.WriteLine(ParseInt("abc"));   // Should be 0
```

### 6. Full Parsing Test

```csharp
var message = "MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1\nPID|1||291235^^^ORA^SR||WALL^MIKE";
var parser = new Hl7Parser();
var parsed = parser.ParseMessage(message);

Console.WriteLine($"Segments: {parsed.Segments.Count}");  // Should be 2
var msh = parsed.GetSegment<MSHSegment>("MSH");
var pid = parsed.GetSegment<PIDSegment>("PID");

if (msh != null)
    Console.WriteLine($"MSH SendingApplication: {msh.SendingApplication}");

if (pid != null)
    Console.WriteLine($"PID PatientName: {pid.PatientName}");
```

## Common Debugging Output Patterns

### Working Correctly
```
SendingApplication: CAIR IIS ✓
SendingFacility: CAIR IIS ✓
ReceivingFacility: DE000001 ✓
VersionId: 2.5.1 ✓
```

### Wrong Field Indices
```
SendingApplication: ^~\& ✗    (Got encoding chars)
SendingFacility: CAIR IIS ✗   (Off by one)
ReceivingFacility:  ✗         (Empty, should be DE000001)
```

### Message Not Parsing
```
Segments: 0 ✗  (Should be >1)
```

### Separators Not Extracted
```
FieldSeparator: ?
ComponentSeparator: ?
```

## How to Fix If Tests Fail

### If SendingApplication == "^~\&"
**Issue**: Using wrong field index
**Fix**: In ParseMSHSegment(), change SendingApplication from GetField(fields, 1) to GetField(fields, 2)

### If PatientName is empty
**Issue**: PID indices wrong or MSH not parsed first
**Fix**: 
1. Ensure ParseMessage() calls ParseMSHSegment() FIRST
2. Verify PID uses indices [1], [3], [5], etc.

### If message.Segments.Count == 0
**Issue**: Line splitting isn't working
**Fix**: Check that Split() is handling both \r\n and \n correctly

### If Separators are default (|, ^, ~, \, &) but should be different
**Issue**: ParseFromMSH is failing or MSH not parsed
**Fix**: Verify ParseFromMSH() is called before parsing other segments

## Validate Each Component

### Parser
```csharp
new Hl7Parser().ParseMSHSegment("MSH|^~\\&|CAIR IIS|CAIR IIS||DE000001|20170509||RSP^K11^RSP_K11|200|P|2.5.1");
// If this works, parser is OK
```

### Separators
```csharp
Hl7Separators.ParseFromMSH("MSH|^~\\&|test|");
// If all separators are correct, this is OK
```

### Field Splitting
```csharp
var fields = "MSH|^~\\&|CAIR IIS".Split('|');
// Check that fields array looks right
```

### GetField
```csharp
GetField(fields, 2) == "CAIR IIS"
// If this works, GetField is OK
```

### Full Message Parsing
```csharp
parser.ParseMessage(fullMessage)
// If this returns segments, full parsing works
```

## Final Verification

Run these commands to verify everything:

```bash
# Run minimal tests
dotnet test Hl7Test/Hl7Test.csproj -k "MinimalMSHTest"

# Run diagnostic tests  
dotnet test Hl7Test/Hl7Test.csproj -k "DiagnosticParsingTests"

# Run all real-world tests
dotnet test Hl7Test/Hl7Test.csproj -k "RealWorldCAIR2Tests"

# Run everything
dotnet test Hl7Test/Hl7Test.csproj
```

Expected: All tests pass ✅
