# Secure Notepad

Secure Notepad is an application written in C# that aims to provide a secure way of storing and encrypting text documents. It functions similarly to Windows Notepad, but with the important feature of encrypting text files using AES standards with a user-defined password and saving them as encrypted files. The saved files have the extension ".stxt".

## Features

- Create, open, and edit text documents securely.
- Encrypt and decrypt text documents using AES encryption.
- Save and load encrypted text documents with the ".stxt" file extension.
- User-friendly interface similar to Windows Notepad.
- Support for both 32-bit and 64-bit architectures.

## Installation

### Prerequisites
- .NET Framework 4.8 or later.

### Installation Steps

1. Download the appropriate installation file for your architecture from the [Releases](https://github.com/korayustundag/securenotepad/releases) page.
2. Run the downloaded installer.
3. Follow the installation wizard instructions.
4. Once the installation is complete, the ".stxt" file extension will be automatically associated with Secure Notepad.

## Build from Source

If you prefer to build Secure Notepad from source, follow these steps:

### Prerequisites

- Visual Studio 2019 or later.
- .NET Framework 4.8 or later.

### Build Steps

1. Clone the repository or download the source code from the [Secure Notepad GitHub page](https://github.com/korayustundag/securenotepad).
2. Open the Secure Notepad solution file (`snotepad.sln`) in Visual Studio.
3. Set the build configuration to either "Release" or "Debug" based on your requirements.
4. Build the solution by selecting **Build > Build Solution** or by pressing F6.
5. Once the build is complete, you will find the executable file in the designated output directory.

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request. [More information](CONTRIBUTING.md)

## Disclaimer

**IMPORTANT: Please read this disclaimer carefully before using Secure Notepad.**

Secure Notepad is a software application designed to encrypt and secure text documents. It utilizes strong encryption algorithms to protect your data. However, it is important to understand the limitations and responsibilities associated with using this application.

### Data Encryption and Password Protection

Secure Notepad encrypts your text documents using AES standards with a password of your choice. The encryption process is designed to provide a high level of security and ensure that only users with the correct password can access the decrypted data.

**Disclaimer:** If you forget or lose your password, it will not be possible to recover or decrypt your data. The encryption is designed in such a way that even the developers of Secure Notepad cannot retrieve or reset your password or access your encrypted data. It is essential to keep your password safe and secure. We highly recommend choosing a strong and memorable password and storing it in a secure location.

## License

Secure Notepad is released under the GPL-3.0 License. See [LICENSE](LICENSE) for more information.
