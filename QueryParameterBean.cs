namespace SignatureGenerator.KuveytTurk;

public class QueryParameterBean
{
    public string ParamName { get; set; }
    public string ParamValue { get; set; }

    public QueryParameterBean()
    {
    }

    public QueryParameterBean(string paramName, string paramValue)
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