namespace ByteShop.Exceptions.Exceptions;
public class DomainExecption : ByteShopException
{
    public DomainExecption(string error) : base(error)
    { 
    }

    public static void When(bool hasError, string error)
    {
        if (hasError)
            throw new DomainExecption(error);
    }
}
