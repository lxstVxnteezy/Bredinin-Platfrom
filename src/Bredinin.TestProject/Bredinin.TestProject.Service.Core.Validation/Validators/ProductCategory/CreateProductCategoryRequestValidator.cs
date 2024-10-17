using Bredinin.TestProject.Service.Contracts.ProductCategory.Create;
using FluentValidation;

namespace Bredinin.TestProject.Service.Core.Validation.Validators.ProductCategory
{
    public class CreateProductCategoryRequestValidator : AbstractValidator<ProductCategoryCreateRequest>
    {
        public CreateProductCategoryRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Category description must not exceed 500 characters.");
        }
    }
}
