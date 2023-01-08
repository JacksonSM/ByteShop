using ByteShop.Domain.DomainMessages;
using FluentAssertions;
using Utilities.Commands;
using Xunit;

namespace Application.Test.Command;
public class UpdateProductCommandValidation
{
    [Fact]
    public void UpdateCommand_ComValoresEmSetMainImageBase64ESetMainImageUrl_CommandInvalido()
    {
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.SetMainImageUrl = "https://www.google.com.br/";

        command.IsValid(new ByteShop.Domain.Entities.Product(),
            new ByteShop.Domain.Entities.Category())
            .Should().BeFalse();

        command.ValidationResult.Errors
            .Should().ContainSingle(ResourceValidationErrorMessage.UPDATE_PRODUCT_WITH_INVALID_MAIN_IMAGE);
    }
}
