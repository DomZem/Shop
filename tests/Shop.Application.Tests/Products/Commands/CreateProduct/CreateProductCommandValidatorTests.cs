using FluentValidation.TestHelper;
using Xunit;

namespace Shop.Application.Products.Commands.CreateProduct.Tests
{
    public class CreateProductCommandValidatorTests
    {
        [Fact]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            // arrange

            var command = new CreateProductCommand()
            {
                Name = "Test",
                ProductCategoryId = 1,
                Description = "test",
                Price = 100,
                Quantity = 1,
            };

            var validator = new CreateProductCommandValidator();
                
            // act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            // arrange

            var command = new CreateProductCommand()
            {
                Name = "Te",
                Description = "    ",
                Price = -100,
                Quantity = -1,
            };

            var validator = new CreateProductCommandValidator();

            // act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(p => p.Name);
            result.ShouldHaveValidationErrorFor(p => p.Description);
            result.ShouldHaveValidationErrorFor(p => p.Price);
            result.ShouldHaveValidationErrorFor(p => p.Quantity);
        }
    }
}