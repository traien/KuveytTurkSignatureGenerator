namespace SignatureGenerator.KuveytTurk;

public class SignatureGenerationException : Exception
{
    public SignatureGenerationException(string message, Exception e)
        : base(message, e)
    {
    }

    public SignatureGenerationException(string message)
        : base(message)
    {
    }

    public override string ToString()
    {
        return $"SignatureGenerationException{{ message='{base.Message}' }}";
    }
}