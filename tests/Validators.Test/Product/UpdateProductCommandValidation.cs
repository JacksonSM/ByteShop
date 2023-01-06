using ByteShop.Exceptions;
using FluentAssertions;
using Utilities.Commands;
using Xunit;

namespace Validators.Test.Product;
public class UpdateProductCommandValidation
{
    [Fact]
    public void UpdateCommand_ComValoresEmSetMainImageBase64ESetMainImageUrl_CommandInvalido()
    {
        var command = ProductCommandBuilder.UpdateProductCommandBuild();
        command.SetMainImageUrl = "https://www.google.com.br/";

        command.IsValid(new ByteShop.Domain.Entities.Product(),true)
            .Should().BeFalse();

        command.ValidationResult.Errors
            .Should().ContainSingle(ResourceErrorMessages.UPDATE_PRODUCT_WITH_INVALID_MAIN_IMAGE);
    }
}
