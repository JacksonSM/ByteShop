using ByteShop.Application.User.Base;
using ByteShop.Exceptions;
using FluentValidation;

namespace ByteShop.Application.User.RegisterUser;
public class RegisterCustomerValidation : AbstractValidator<RegisterCustomerCommand>
{
    public RegisterCustomerValidation()
    {
        RuleFor(c => c.Cpf).SetValidator(new CPFValidator());

        RuleFor(c => c.UserName)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_USERNAME);

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMPTY_EMAIL);

        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(ResourceErrorMessages.INVALID_EMAIL);
        });

    }
}
