namespace ByteShop.Exceptions.Exceptions;
public class DomainExecption : ByteShopException
{
    public string ErrorMessage;
    public DomainExecption(string error) : base(string.Empty)
    { 
        ErrorMessage = error;
    }

    public static void When(bool hasError, string error)
    {
        if (hasError)
            throw new DomainExecption(error);
    }
}
