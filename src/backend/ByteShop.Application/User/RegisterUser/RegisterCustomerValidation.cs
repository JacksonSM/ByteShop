using ByteShop.Application.User.Base;
using ByteShop.Domain.DomainMessages;
using FluentValidation;

namespace ByteShop.Application.User.RegisterUser;
public class RegisterCustomerValidation : AbstractValidator<RegisterCustomerCommand>
{
    public RegisterCustomerValidation()
    {
        RuleFor(c => c.Cpf).SetValidator(new CPFValidator());

        RuleFor(c => c.UserName)
            .NotEmpty()
            .WithMessage(ResourceValidationErrorMessage.EMPTY_USERNAME);

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage(ResourceValidationErrorMessage.EMPTY_EMAIL);

        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(ResourceValidationErrorMessage.INVALID_EMAIL);
        });

    }
}
