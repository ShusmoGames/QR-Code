# QR Code Scanner and Generator in Unity

This Unity project provides functionality to scan QR codes using a device camera and generate QR codes from text. It supports both mobile iOS and Android platforms for scanning and generating QR codes.

## Features

- QR Code Scanning: Scan QR codes using the device camera.
- QR Code Generation: Generate QR codes from text.
- Cross-Platform: Works on Android and iOS.

## Example of Using

### QR Code Scanning
```csharp
ShusmoAPI.QR.Scan
(
onSuccess: (result) => { Debug.Log("Scan successful: " + result); },
onFailed: (error) => { Debug.LogError("Scan failed: " + error); },
onFouresStop: (reason) => { Debug.Log("Scanning stopped: " + reason); }
);
```

### QR Code Generate
```csharp
Texture2D texture = ShusmoAPI.QR.Generate(qrInputField.text);
```

### QR Code Read
```csharp
string text = ShusmoAPI.QR.Read(texture);
```
