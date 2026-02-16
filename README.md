# CAIR2 HL7v2 Parser & Viewer

![.NET Status](https://github.com/tngo0508/CAIR2-HL7-parser/actions/workflows/master_cair2-hl7.yml/badge.svg?branch=master)
[![Target](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/10.0)
[![Platform](https://img.shields.io/badge/Platform-Linux%20%7C%20Windows%20%7C%20macOS-green.svg)]()
[![Demo](https://img.shields.io/badge/Live-Demo-brightgreen.svg)](https://cair2-hl7-gfc5aqc3bteca6c7.canadacentral-01.azurewebsites.net/)

A dedicated .NET HL7v2 message parser and viewer, specifically optimized for the California Immunization Registry (CAIR2). Built on .NET 10, it aims to provide reliable parsing, validation, and serialization to support immunization data exchange.

---

## Core Capabilities

*   **Performance Oriented**: Designed with efficient string processing and reflection-optimized attribute mapping.
*   **CAIR2 Optimized**: Out-of-the-box support for VXU, QBP, and RSP messages used by California's registry.
*   **Interactive Viewer**: Includes a modern ASP.NET Core MVC web application for visual message inspection.
*   **Full Segment Support**: Dedicated models for `MSH`, `PID`, `RXA`, `OBX`, `OBR`, `ORC`, and many more.
*   **Validation Support**: Includes a validation engine to help ensure HL7 compliance and alignment with CAIR2 implementation guides.
*   **Bidirectional**: Support for both parsing incoming messages and serializing outgoing queries/responses.
*   **Cloud Ready**: Optimized for deployment to Azure App Service on Linux via GitHub Actions.

---

## Web Viewer

[**Explore the Live Demo**](https://cair2-hl7-gfc5aqc3bteca6c7.canadacentral-01.azurewebsites.net/)

The project includes a built-in HL7 viewer (`Hl7.Mvc`) that features:
- **Real-time Parsing**: Paste and analyze HL7 messages instantly.
- **Interactive Hierarchy**: Explore segments using a Fluent UI-powered tree-view.
- **Visual Breakdown**: Side-by-side view of raw vs. parsed data with detailed field/component inspection.
- **Responsive Design**: Modern, clean interface suitable for clinical data analysis.

---

## Quick Start

### 1. Installation
Add a reference to the `Hl7.Core` library in your project:
```bash
dotnet add reference Hl7.Core/Hl7.Core.csproj
```

### 2. Basic Parsing
```csharp
using Hl7.Core;

var parser = new Hl7Parser();
var hl7Message = "MSH|^~\\&|EMR|SENDER||CAIR2|2023... \nPID|1||123...";

// Parse the complete message
var message = parser.ParseMessage(hl7Message);

// Access typed data
var pid = message.GetSegment<PIDSegment>("PID");
Console.WriteLine($"Patient: {pid.PatientName}");
```

---

## Advanced Usage

### CAIR2-Specific Logic
```csharp
using Hl7.Core.CAIR2;

var cair2Parser = new CAIR2Parser();
var message = cair2Parser.ParseVaccinationMessage(rawHl7);

if (cair2Parser.ValidateCAIR2Message(message))
{
    var records = cair2Parser.ExtractVaccinationRecords(message);
    // Process clinical records...
}
```

### Serialization
```csharp
var serializer = new Hl7MessageSerializer(new Hl7Separators());
var hl7String = serializer.Serialize(message);
```

---

## Architecture & Segments

### Supported Segments
| Category | Segments |
|---|---|
| **Header/Control** | `MSH`, `MSA`, `ERR` |
| **Patient** | `PID`, `PD1`, `NK1` |
| **Clinical** | `RXA`, `RXR`, `OBX`, `OBR`, `ORC` |
| **Query/Response** | `QPD`, `QAK`, `RCP` |

### Technology Stack
- **Backend**: .NET 10.0 (C# 14.0)
- **Frontend**: ASP.NET Core MVC, Fluent UI Web Components, Bootstrap 5
- **Testing**: xUnit with 60+ test cases covering various scenarios
- **CI/CD**: GitHub Actions targeting Azure App Service (Linux-x64)

---

## Reliability & Testing

A suite of tests is provided to help maintain stability and ensure compliance:
```bash
dotnet test Hl7Test/Hl7Test.csproj
```

---

## Contributing & Feedback

This project is an ongoing effort to provide a helpful tool for the healthcare community. While we strive for accuracy, we recognize there is always room for improvement. We humbly welcome feedback, bug reports, and contributions to help make this parser more reliable for everyone.

---

## References
- [Live Demo](https://cair2-hl7-gfc5aqc3bteca6c7.canadacentral-01.azurewebsites.net/)
- [CAIR2 Implementation Guide](https://cairweb.org/)
- [CDC HL7 Implementation Guide](https://www.cdc.gov/iis/technical-guidance/hl7.html)
- [HL7 v2.5.1 Specification](https://www.hl7.org/)
- [HL7Parser by LaMMMy](https://github.com/LaMMMy/HL7Parser)
- [Online HL7 Viewer](https://www.hl7viewer.com/)

---
*Built with a commitment to supporting public health through better data exchange.*
