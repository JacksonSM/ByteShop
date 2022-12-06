namespace ByteShop.Exceptions.ExceptionsBase;

public class ValidationErrorsException : ByteShopException
{
    public List<string> ErrorMessages { get; set; }

    public ValidationErrorsException(List<string> errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }
}
