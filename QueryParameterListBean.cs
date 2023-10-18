namespace SignatureGenerator.KuveytTurk;

public class QueryParameterListBean
{
    private readonly Dictionary<string, object> parameters;

    public QueryParameterListBean()
    {
        parameters = new Dictionary<string, object>();
    }

    public QueryParameterListBean(string apiEndpointUrl)
        : this()
    {
        ParseApiEndpointUrl(apiEndpointUrl);
    }

    private void ParseApiEndpointUrl(string apiEndpointUrl)
    {
        if (apiEndpointUrl.Contains("?"))
        {
            string[] urlTokens = apiEndpointUrl.Split('?');
            if (urlTokens.Length == 2)
            {
                string parameterSequence = urlTokens[1];
                string[] parameterPairs = parameterSequence.Split('&');

                if (parameterPairs.Length > 0)
                {
                    foreach (string paramPair in parameterPairs)
                    {
                        string[] paramTokens = paramPair.Split('=');
                        if (paramTokens.Length == 2)
                        {
                            string paramName = paramTokens[0];
                            string paramValue = paramTokens[1];
                            parameters[paramName] = paramValue;
                        }
                        else
                        {
                            string errMsg = "Invalid parameter format has been detected in the query parameter: " + paramPair;
                            throw new SignatureGenerationException(errMsg);
                        }
                    }
                }
                else
                {
                    string errMsg = "Query parameters must be provided after the question mark!";
                    throw new SignatureGenerationException(errMsg);
                }
            }
            else
            {
                string errMsg = "Endpoint URL is in an invalid format. It must contain zero or only one question mark!";
                throw new SignatureGenerationException(errMsg);
            }
        }
    }

    public QueryParameterListBean Add(QueryParameterBean bean)
    {
        if (bean != null)
        {
            parameters[bean.ParamName] = bean.ParamValue;
        }

        return this;
    }

    public QueryParameterListBean Remove(string key)
    {
        parameters.Remove(key);
        return this;
    }

    public QueryParameterListBean Clear()
    {
        parameters.Clear();
        return this;
    }

    public bool IsEmpty => parameters.Count == 0;

    public List<QueryParameterBean> ToList()
    {
        return parameters.Select(kv => new QueryParameterBean(kv.Key, kv.Value.ToString())).ToList();
    }

    public override string ToString()
    {
        List<QueryParameterBean> queryParams = ToList();

        if (queryParams != null && queryParams.Count > 0)
        {
            return "?" + string.Join("&", queryParams.Select(param => $"{param.ParamName}={param.ParamValue}"));
        }

        return "";
    }
}