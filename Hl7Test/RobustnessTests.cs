using Hl7.Core;
using Hl7.Core.Base;
using Hl7.Core.Segments;
using Hl7.Core.Common;

namespace Hl7Test;

public class RobustnessTests
{
    private readonly Hl7Parser _parser = new Hl7Parser();

    [Fact]
    public void Parse_Message_WithTrailingEmptyFields_ShouldNotThrow()
    {
        // Many HL7 messages have fewer fields than the maximum defined in the spec
        string messageWithShortPID = "MSH|^~\\&|EMR|FAC||CAIR2|20230101||VXU^V04|123|P|2.5.1\nPID|1||12345";
        
        var message = _parser.ParseMessage(messageWithShortPID);
        
        Assert.NotNull(message);
        var pid = message.GetSegment<PIDSegment>("PID");
        Assert.NotNull(pid);
        Assert.Equal("12345", pid.PatientIdentifierList);
        Assert.Equal(string.Empty, pid.PatientName); // Optional field beyond provided data
    }

    [Fact]
    public void Parse_Message_WithExtraFields_ShouldNotThrow()
    {
        // Extra fields should be ignored but captured in the generic Fields dictionary
        string messageWithExtraFields = "MSH|^~\\&|EMR|FAC||CAIR2|20230101||VXU^V04|123|P|2.5.1\nPID|1||12345||DOE^JOHN||19800101|M|||||||||||||||||EXTRA_FIELD";
        
        var message = _parser.ParseMessage(messageWithExtraFields);
        
        Assert.NotNull(message);
        var pid = message.GetSegment<PIDSegment>("PID");
        Assert.NotNull(pid);
        Assert.Equal("DOE^JOHN", pid.PatientName);
        // Field 25 is beyond what PIDSegment normally maps, but should be in the dictionary if the parser works correctly
        Assert.Equal("EXTRA_FIELD", pid.GetField(25));
    }

    [Fact]
    public void Parse_Message_WithCustomSeparators_ShouldHandleCorrectly()
    {
        // Custom separators are allowed in HL7
        // MSH followed by field separator '.', then encoding chars
        string messageWithCustomSeparators = "MSH.~\\&.EMR.FAC..CAIR2.20230101..VXU^V04.123.P.2.5.1\nPID.1..12345";
        
        // Note: Hl7Parser might need to be initialized with these or it might detect them.
        // Looking at Hl7Parser.cs, it uses '|' by default. Let's see if it's robust.
        
        var message = _parser.ParseMessage(messageWithCustomSeparators);
        
        // If the parser isn't currently robust enough to detect separators from MSH automatically, 
        // this test might fail. That's good for identifying "robustness" gaps.
        Assert.NotNull(message);
        var msh = message.GetSegment<MSHSegment>("MSH");
        Assert.Equal("EMR", msh.SendingApplication);
    }

    [Fact]
    public void Parse_EmptyMessage_ShouldReturnEmptyHl7Message()
    {
        var message = _parser.ParseMessage("");
        Assert.NotNull(message);
        Assert.Empty(message.Segments);
    }

    [Fact]
    public void Parse_MalformedMSH_ShouldReturnGenericSegments()
    {
        // If MSH is missing or malformed, it might not be parsed as MSHSegment but should still be parsed
        string malformed = "XXX|1|2|3";
        var message = _parser.ParseMessage(malformed);
        
        Assert.NotNull(message);
        Assert.Single(message.Segments);
        Assert.Equal("XXX", message.Segments[0].SegmentId);
    }

    [Fact]
    public void Parse_Message_WithEscapedCharacters_ShouldUnescape()
    {
        string messageWithEscapes = "MSH|^~\\&|EMR|FAC||CAIR2|20230101||VXU^V04|123|P|2.5.1\nPID|1||12345||DOE^JOHN\\F\\JR||19800101|M";
        
        var message = _parser.ParseMessage(messageWithEscapes);
        
        var pid = message.GetSegment<PIDSegment>("PID");
        Assert.NotNull(pid);
        // \F\ is field separator escape. If unescaped, it should be '|'
        Assert.Equal("DOE^JOHN|JR", pid.PatientName);
    }

    [Fact]
    public void Parse_EveryMappedField_MSH_Test()
    {
        string hl7 = "MSH|^~\\&|APP|FAC|RAPP|RFAC|202301011200|SEC|VXU^V04|MSGID|P|2.5.1|SEQ|CONT|ER|AL|CC|CHAR|LANG|SCHEME|PROFILE";
        var message = _parser.ParseMessage(hl7);
        var msh = message.GetSegment<MSHSegment>("MSH");
        
        Assert.NotNull(msh);
        Assert.Equal("^~\\&", msh.EncodingCharacters);
        Assert.Equal("APP", msh.SendingApplication);
        Assert.Equal("FAC", msh.SendingFacility);
        Assert.Equal("RAPP", msh.ReceivingApplication);
        Assert.Equal("RFAC", msh.ReceivingFacility);
        Assert.Equal("202301011200", msh.MessageDateTime);
        Assert.Equal("SEC", msh.Security);
        Assert.Equal("VXU^V04", msh.MessageType);
        Assert.Equal("MSGID", msh.MessageControlId);
        Assert.Equal("P", msh.ProcessingId);
        Assert.Equal("2.5.1", msh.VersionId);
        Assert.Equal("SEQ", msh.SequenceNumber);
        Assert.Equal("CONT", msh.ContinuationPointer);
        Assert.Equal("ER", msh.AcceptAcknowledgmentType);
        Assert.Equal("AL", msh.ApplicationAcknowledgmentType);
        Assert.Equal("CC", msh.CountryCode);
        Assert.Equal("CHAR", msh.CharacterSet);
        Assert.Equal("LANG", msh.PrincipalLanguageOfMessage);
        Assert.Equal("SCHEME", msh.AlternateCharacterSetHandlingScheme);
        Assert.Equal("PROFILE", msh.MessageProfileIdentifier);
    }

    [Fact]
    public void Parse_MultipleSegmentsOfSameType_ShouldAllBeAccessible()
    {
        string hl7 = "MSH|^~\\&|EMR|FAC||CAIR2|20230101||VXU^V04|123|P|2.5.1\nNK1|1|DOE^JANE|MTH\nNK1|2|DOE^JAMES|FTH";
        var message = _parser.ParseMessage(hl7);
        
        var nk1s = message.GetSegments<NK1Segment>("NK1");
        Assert.Equal(2, nk1s.Count);
        Assert.Equal("DOE^JANE", nk1s[0].Name);
        Assert.Equal("DOE^JAMES", nk1s[1].Name);
    }

    [Fact]
    public void Parse_InvalidNumericData_ShouldReturnZeroInsteadOfThrowing()
    {
        // PID-1 is an int. If we provide "ABC", it should be 0.
        string hl7 = "MSH|^~\\&|EMR|FAC||CAIR2|20230101||VXU^V04|123|P|2.5.1\nPID|ABC||12345";
        var message = _parser.ParseMessage(hl7);
        
        var pid = message.GetSegment<PIDSegment>("PID");
        Assert.NotNull(pid);
        Assert.Equal(0, pid.SetId);
    }
}
