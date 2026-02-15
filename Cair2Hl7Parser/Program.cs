using Hl7.Core;
using Hl7.Core.Segments;

namespace Cair2Hl7Parser;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var parser = new Hl7Parser();
        var message = parser.ParseMessage("MSH|^~\\&|SendingSystem|SendingFacility|CAIR2|CDPH|20240901100000||VXU^V04^VXU_V04|MSGID001|P|2.5.1|||NE|NE|||CDPH_2_5_1|\nPID|1||PID123^^^MRN||DOE^JOHN^||20200101|M||2106-3^White^CD9^...|123 Main St^Anytown^CA^90000^USA^H||^PRN^PH^^1^555^5555|||||||...|2135-2^Hispanic or Latino^CD9^...|||||||\nPV1|1|R||||||||||||||||||||||||||||||||||||||||||\nORC|RE|||1234567890^CLINIC|||||||123456^DR^TEST^^^^^^NPI|||||||\nRXA|0|1|20240901|20240901|108^Influenza, seasonal, inactivated, nasal^CVX|0.5|mL^milliliters^UCUM||00^Administered^NIP001||||||||||||||\nRXR|C28161^Intranasal^NCIT|LA^Left Arm^HL70163|\nOBX|1|CE|64994-7^Vaccine CVX Code^LN|1|108^Influenza, seasonal, inactivated, nasal^CVX||||||F|||20240901|\nOBX|2|CE|30956-7^Vaccine Type^LN|2|108^Influenza, seasonal, inactivated, nasal^CVX||||||F|||20240901|\nOBX|3|CE|69764-9^Administered Date^LN|3|20240901||||||F|||20240901|\n");
        
        PIDSegment? pid = message.GetSegment<PIDSegment>("PID");
        RXASegment? rxa = message.GetSegment<RXASegment>("RXA");

        if (pid != null)
        {
            Console.WriteLine("PID Segment Properties:");
            foreach (var property in pid.GetType().GetProperties())
            {
                var value = property.GetValue(pid);
                Console.WriteLine($"{property.Name}: {value}");
            }

            Console.WriteLine(pid.PatientAddress.City);
            Console.WriteLine(pid.PatientAddress.StreetAddress);
        }
    }
}