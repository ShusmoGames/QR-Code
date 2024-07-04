# QR Code Scanner and Generator in Unity

This Unity project provides functionality to scan QR codes using a device camera and generate QR codes from text. It supports both mobile iOS and Android platforms for scanning and generating QR codes.

## Features

- QR Code Scanning: Scan QR codes using the device camera.
- QR Code Generation: Generate QR codes from text.
- Cross-Platform: Works on Android and iOS.

## Example of Using

### QR Code Scanning
```csharp
using ShusmoSystems.QR;

QR.Scan
(
onSuccess: (result) => { Debug.Log("Scan successful: " + result); },
onFailed: (error) => { Debug.LogError("Scan failed: " + error); },
onFouresStop: (reason) => { Debug.Log("Scanning stopped: " + reason); }
);
```

### QR Code Generate
```csharp
using ShusmoSystems.QR;

Texture2D texture = QR.Generate(qrInputField.text);
```

### QR Code Read
```csharp
using ShusmoSystems.QR;

string text = QR.Read(texture);
```
