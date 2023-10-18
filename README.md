# SignatureGenerator.KuveytTurk Library

SignatureGenerator.KuveytTurk is a C# library that provides utility classes for generating digital signatures and working with query parameters in the context of Kuveyt Türk API integration. This library is designed to simplify the process of generating signatures for API requests.

## Installation

You can install this library via NuGet Package Manager in Visual Studio or by using the `dotnet` CLI.

```shell
nuget install SignatureGenerator.KuveytTurk
```

OR

```shell
dotnet add package SignatureGenerator.KuveytTurk
```

## Usage

### Generate API Request Signature

Use the `GenerateApiRequestSignature` method to generate an API request signature. It supports both GET and POST requests.

```csharp
using SignatureGenerator.KuveytTurk;

// ...

try
{
    string httpMethod = "GET"; // or "POST"
    string accessToken = "your-access-token";
    string privateKey = "your-private-key";
    string requestBody = "request-body"; // Required for POST requests
    string apiEndpointUrl = "api-endpoint-url"; // Required for GET requests with query parameters

    string signature = SignatureGeneratorUtility.GenerateApiRequestSignature(httpMethod, accessToken, privateKey, apiEndpointUrl, requestBody);

    Console.WriteLine("Generated Signature:");
    Console.WriteLine(signature);
}
catch (SignatureGenerationException e)
{
    Console.WriteLine("Error occurred while generating the request signature:");
    Console.WriteLine(e.Message);
}
```

## Contributing

If you want to contribute to this project, please open an issue or submit a pull request on the GitHub repository.

## Authors

- Osama Bashir

## Contact

For any questions or support, please contact traien@outlook.com.

## License

This library is provided under the MIT License.

## Support and Contribution

For any questions, issues, or contributions, please [submit an issue](https://github.com/traien/KuveytTurkSignatureGenerator/issues/new) on GitHub.

## References

- [Kuveyt Türk API Documentation](https://developer.kuveytturk.com.tr/documentation)

---

### Example

Here's a complete example of generating an API request signature:

```csharp
using System;
using SignatureGenerator.KuveytTurk;

class Program
{
    static void Main()
    {
        try
        {
            string httpMethod = "POST";
            string accessToken = "your-access-token";
            string privateKey = "-----BEGIN PRIVATE KEY-----***-----END PRIVATE KEY-----"; // Replace with your actual private key
            string requestBody = "{ \"key\": \"value\" }"; // Replace with your actual request body
            string apiEndpointUrl = "https://api.example.com/endpoint?param1=value1&param2=value2"; // Replace with your actual API endpoint URL

            string signature = SignatureGeneratorUtility.GenerateApiRequestSignature(httpMethod, accessToken, privateKey, apiEndpointUrl, requestBody);

            Console.WriteLine("Generated Signature:");
            Console.WriteLine(signature);
        }
        catch (SignatureGenerationException e)
        {
            Console.WriteLine("Error occurred while generating the request signature:");
            Console.WriteLine(e.Message);
        }
    }
}
```
### How to Generate a Public/Private Key Pair for Signature Generation

In order to generate a signature, you need to create a public/private key pair. The following steps guide you on how to use the OpenSSL tool chain to generate these key pairs for testing purposes. We provide instructions for both Windows and macOS:

#### Windows:

1. Download the setup file named `openssl-0.9.8h-1-setup.exe` from [OpenSSL](https://slproweb.com/products/Win32OpenSSL.html) and install it on your PC.

2. Add `C:\Program Files (x86)\GnuWin32\bin` to your PATH environment variable.

3. In your `D` drive, create a folder named "test" (i.e., `D:\test`).

4. Copy `openssl.cnf` from `C:\Program Files (x86)\GnuWin32\share` to `D:\test`.

5. Open a command prompt while under `D:\test`.

6. Execute the following commands in the specified order from `a` to `g`:

   a) `openssl genrsa -des3 -out server.key 1024` (private key generated)

   b) `openssl req -config "D:\test\openssl.cnf" -new -key server.key -out server.csr`

   c) `copy server.key server.key.org`

   d) `openssl rsa -in server.key.org -out server.key` (Private Key turned into PEM format that is PKCS#1 compliant)

   e) `openssl pkcs8 -topk8 -inform PEM -outform PEM -nocrypt -in server.key -out server.key.8` (PKCS#8 compliant private key generated)

   f) `openssl x509 -req -days 365 -in server.csr -signkey server.key -out server.crt` (Self-signed certificate generated)

   g) `openssl x509 -pubkey -noout -in server.crt > pubkey.pem` (Public key generated)

#### macOS:

1. Open a terminal.

2. Execute the following commands in the specified order from `a` to `g`:

   a) `openssl genrsa -des3 -out server.key 1024` (private key generated)

   b) `openssl req -new -key server.key -out server.csr`

   c) `cp server.key server.key.org`

   d) `openssl rsa -in server.key.org -out server.key` (Private Key turned into PEM format that is PKCS#1 compliant)

   e) `openssl pkcs8 -topk8 -inform PEM -outform PEM -nocrypt -in server.key -out server.key.8` (PKCS#8 compliant private key generated)

   f) `openssl x509 -req -days 365 -in server.csr -signkey server.key -out server.crt` (Self-signed certificate generated)

   g) `openssl x509 -pubkey -noout -in server.crt > pubkey.pem` (Public key generated)

#### Outcome:

Following files are generated at `D:\test` (Windows) or your current working directory (macOS) as a result of the above command executions from `a` to `g`:

- `server.crt` ==> Certificate
- `server.key` ==> Private Key that is compliant with PKCS#1 standard
- `server.key.8` ==> Private Key that is compliant with PKCS#8 standard
- `pubkey.pem` ==> Public Key

#### Final Steps:

- Save the content of the file `pubkey.pem` into the public key field of your application that you had created earlier at API Market.

- Use the content of the file `server.key.8` (i.e., private key) to generate your signature.
