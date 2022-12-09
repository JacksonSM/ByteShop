namespace ByteShop.Domain.Exceptions;
public class DomainExecption : Exception
{
    public DomainExecption(string error) : base(error)
    { }

    public static void When(bool hasError, string error)
    {
        if (hasError)
            throw new DomainExecption(error);
    }
}
