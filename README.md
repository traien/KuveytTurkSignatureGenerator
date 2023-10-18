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

    string signature = SignatureGeneratorUtility.GenerateApiRequestSignature(httpMethod, accessToken, privateKey, requestBody, apiEndpointUrl);

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

## License

This library is available under the MIT License. See the [LICENSE](LICENSE) file for more information.

## Authors

- Osama Bashir

## Contact

For any questions or support, please contact traien@outlook.com.

## License

This library is provided under the MIT License.

## Support and Contribution

For any questions, issues, or contributions, please [submit an issue](https://github.com/your-repo-link) on GitHub.

## References

- [Kuveyt Türk API Documentation](https://example.com/api-docs)

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

            string signature = SignatureGeneratorUtility.GenerateApiRequestSignature(httpMethod, accessToken, privateKey, requestBody, apiEndpointUrl);

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

