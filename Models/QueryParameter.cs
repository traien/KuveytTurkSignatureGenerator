namespace SignatureGenerator.KuveytTurk.Models;

public class QueryParameter
{
    public string ParamName { get; set; }
    public string ParamValue { get; set; }

    public QueryParameter()
    {
    }

    public QueryParameter(string paramName, string paramValue)
    {
        ParamName = paramName;
        ParamValue = paramValue;
    }

    // Public function to generate a query parameter string
    public override string ToString()
    {
        return $"{ParamName}={ParamValue}";
    }
}