using System.Security.Cryptography;
using System.Text;
using SignatureGenerator.KuveytTurk.Exceptions;
using SignatureGenerator.KuveytTurk.Models;

namespace SignatureGenerator.KuveytTurk;

public class SignatureGeneratorUtility
{
    public static string GenerateApiRequestSignature(string httpMethod, string accessToken, string privateKey, string apiEndpointUrl = null, string requestBody = null)
    {
        try
        {
            ValidateInputs(httpMethod, accessToken, privateKey, requestBody, apiEndpointUrl);

            if (httpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                return GenerateSignature(accessToken, privateKey, requestBody);
            }

            if (httpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                return GenerateGetSignature(accessToken, privateKey, apiEndpointUrl);
            }

            throw new SignatureGenerationException("Invalid HTTP method selected.");
        }
        catch (SignatureGenerationException e)
        {
            throw e;
        }
    }

    private static void ValidateInputs(string httpMethod, string accessToken, string privateKey, string requestBody, string apiEndpointUrl)
    {
        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(privateKey))
            throw new SignatureGenerationException("Access token and private key are required.");

        if (httpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(requestBody))
            throw new SignatureGenerationException("Request body is required for POST requests.");

        if (httpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(apiEndpointUrl))
            throw new SignatureGenerationException("API endpoint URL is required for GET requests.");
    }

    private static string GenerateSignature(string accessToken, string privateKey, string requestBody)
    {
        string input = accessToken + requestBody;
        return SignSHA256RSA(input, privateKey);
    }

    private static string GenerateGetSignature(string accessToken, string privateKey, string apiEndpointUrl)
    {
        var queryParams = new QueryParameterList(apiEndpointUrl);
        string queryString = GetQueryParamsString(queryParams);
        string input = accessToken + queryString;
        return SignSHA256RSA(input, privateKey);
    }

    private static string SignSHA256RSA(string input, string privateKey)
    {
        privateKey = privateKey
            .Replace("-----BEGIN PRIVATE KEY-----", "")
            .Replace("-----END PRIVATE KEY-----", "")
            .Replace("\n", "");
        
        using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

        rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);

        byte[] data = Encoding.UTF8.GetBytes(input);
        byte[] signatureBytes = rsa.SignData(data, new SHA256CryptoServiceProvider());

        return Convert.ToBase64String(signatureBytes);
    }

    private static string GetQueryParamsString(QueryParameterList queryParams)
    {
        List<QueryParameter> queryParamsList = queryParams.ToList();
        if (queryParamsList.Count > 0)
        {
            return "?" + string.Join("&", queryParamsList);
        }

        return "";
    }
}