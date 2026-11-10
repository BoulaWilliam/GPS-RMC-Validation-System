# 🛰️ GPS RMC Validation & Parsing System

### A structured .NET Console Application for validating, parsing, and displaying NMEA 0183 RMC GPS data.

---

## 📌 Overview

GPS devices communicate location and navigation information through standardized NMEA sentences. However, incoming data should not be trusted before its structure, integrity, and individual fields have been checked.

**GPS RMC Validation & Parsing System** is a C#/.NET console application designed to receive NMEA RMC messages, validate their integrity and data fields, parse the information, and display the results in a readable format.

The project focuses on clean code, separation of responsibilities, object-oriented programming, and defensive input handling.

## 🎯 Project Objectives

- Accept GPS data through console input or a text file.
- Identify supported RMC message types.
- Verify message integrity using the NMEA checksum.
- Validate the structure and individual RMC fields.
- Parse validated data into a dedicated model.
- Display the extracted GPS information in a clear format.
- Handle invalid input without unnecessarily terminating the application.

## ✨ Key Features

| Feature | Description |
|---|---|
| Multiple Input Sources | Supports console and file-based input. |
| RMC Identification | Recognizes `$GPRMC` and `$GNRMC` messages. |
| Checksum Validation | Calculates and verifies the NMEA checksum. |
| Field Validation | Checks the validity of RMC data fields. |
| GPS Parsing | Extracts the message fields into a structured model. |
| Formatted Output | Displays parsed GPS information in a readable layout. |
| Defensive Handling | Handles invalid input, parsing errors, and file access errors. |
| Protocol Guidance | Provides expected RMC message format information when an unsupported message type is entered. |

## 🏗️ Project Architecture

The application is organized into separate components, each responsible for a specific part of the workflow.

```text
GPS_RMC_Validation_System/
│
├── Display/
│   └── RmcDisplay.cs
│
├── Input/
│   ├── ConsoleInput.cs
│   └── FileInput.cs
│
├── Interface/
│   └── IGpsInput.cs
│
├── Models/
│   └── RmcData.cs
│
├── Parsing/
│   └── RmcParser.cs
│
├── Services/
│   └── RmcApplication.cs
│
├── Validation/
│   ├── ChecksumValidator.cs
│   └── RmcValidator.cs
│
├── Program.cs
└── README.md
```

### Component Responsibilities

**Program.cs**
- Application entry point.
- Initializes and runs the application.

**Input**
- `ConsoleInput`: Receives GPS messages directly from the console.
- `FileInput`: Reads GPS messages from a text file.

**Interface**
- `IGpsInput`: Defines a common contract for different input sources.

**Validation**
- `ChecksumValidator`: Verifies message checksum integrity.
- `RmcValidator`: Validates message type and RMC data fields.

**Parsing**
- `RmcParser`: Extracts the RMC fields and maps them into the data model.

**Models**
- `RmcData`: Represents the structured RMC message data.

**Display**
- `RmcDisplay`: Presents the validated GPS information.

**Services**
- `RmcApplication`: Coordinates the application workflow.

## 🔄 Application Workflow

The application follows a sequential processing pipeline:

```text
        Start Application
               |
               v
         Select Input Source
               |
          +----+----+
          |         |
       Console     File
          |         |
          +----+----+
               |
               v
        Receive GPS Message
               |
               v
        Identify RMC Message
               |
               v
       Validate Checksum
               |
               v
          Parse Message
               |
               v
       Validate RMC Fields
               |
               v
        Display GPS Data
               |
               v
              End
```

Invalid messages are rejected at the relevant validation stage.

## 📡 NMEA RMC Message Format

The project follows the classic 12-field RMC sentence structure.

Example:

```text
$GPRMC,123519,A,4807.038,N,01131.000,E,022.4,084.4,230394,003.1,W*6A
```

### Field Description

| Field | Description | Example |
|---|---|---|
| Message ID | RMC sentence identifier | `$GPRMC` |
| UTC Time | Time of the fix | `123519` |
| Status | Fix validity indicator | `A` |
| Latitude | Latitude in NMEA format | `4807.038` |
| Latitude Direction | North or South | `N` |
| Longitude | Longitude in NMEA format | `01131.000` |
| Longitude Direction | East or West | `E` |
| Speed | Speed over ground in knots | `022.4` |
| Course | Course over ground in degrees | `084.4` |
| Date | Date in DDMMYY format | `230394` |
| Magnetic Variation | Magnetic variation | `003.1` |
| Magnetic Direction | East or West | `W` |
| Checksum | XOR checksum | `6A` |

### Important RMC Status Values

- `A` — Active: valid position fix.
- `V` — Void: invalid position fix.

A structurally valid RMC sentence with status `V` is not necessarily a malformed sentence. The application distinguishes the sentence's validity from the validity of its position fix.

## 🧪 Example Input

```text
$GPRMC,123519,A,4807.038,N,01131.000,E,022.4,084.4,230394,003.1,W*6A
```

### Expected Processing

1. Receive the GPS sentence.
2. Identify the RMC message type.
3. Validate its checksum.
4. Parse the sentence fields.
5. Validate the extracted data.
6. Display the resulting GPS information.

## 🛠️ Technologies & Concepts

- C#
- .NET Console Application
- Object-Oriented Programming (OOP)
- Interfaces and Abstraction
- Separation of Responsibilities
- SOLID Design Principles
- NMEA 0183 RMC Message Format
- File Handling
- Exception Handling
- Data Validation and Parsing

## 🚀 Getting Started

### Prerequisites

- .NET SDK compatible with the project's target framework.
- Visual Studio or another compatible C# IDE.
- Git (optional, for cloning the repository).

### Installation

Clone the repository:

```bash
git clone https://github.com/YOUR_USERNAME/GPS-RMC-Validation-System.git
```

Navigate to the project directory:

```bash
cd GPS-RMC-Validation-System
```

Run the application:

```bash
dotnet run
```

Alternatively, open the `.csproj` file in Visual Studio and run the project.

## 📥 Input Options

When the application starts, the user can select:

**1. Console Input**

Enter an NMEA RMC sentence directly into the console.

**2. File Input**

Provide the path to a text file containing an NMEA RMC sentence.

## 📚 Standards & References

The implementation is based on the NMEA 0183 RMC sentence format.

- [Trimble – NMEA RMC Message Reference](https://receiverhelp.trimble.com/oem-gnss/nmea0183-messages-rmc.html)
- [NMEA 0183 Sentence Reference](https://w3.cs.jmu.edu/bernstdh/web/common/help/nmea-sentences.php)

**Implementation Scope:** This project currently targets the classic 12-field RMC sentence structure. Newer NMEA extensions, including additional mode and navigation status fields, are not claimed as supported unless explicitly implemented and tested.

## 🔮 Future Improvements

- Add automated unit tests for validation and parsing.
- Support multiple NMEA sentences from a single file.
- Extend compatibility with newer RMC sentence variations.
- Introduce dependency injection for application services.
- Improve validation error messages with field-specific details.
- Add structured logging.

## 👨‍💻 Author

**Boula William Reyad**

---

*Built to practice structured software development, defensive programming, and reliable GPS message processing.*
