using Hl7.Core;

namespace Hl7Test;

public class UnitTest1
{
    [Fact]
    public void MSH_Segment_Test()
    {
        // Arrange
        var parser = new Hl7Parser();
        // Act
        var mshSegment = parser.parseMSHSegment("MSH|^~\\&|MyEMR|DE-000001| |CAIR2|201607011230300700||VXU^V04^VXU_V04|CA0001|P|2.5.1|||ER|AL|||||Z22^CDCPHINVS|DE-000001 ");
        // Assert
        Assert.NotNull(mshSegment);
    }
}