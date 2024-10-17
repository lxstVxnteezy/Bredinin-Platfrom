using Bredinin.TestProject.Service.Contracts.ProductCategory.Update;
using FluentValidation;

namespace Bredinin.TestProject.Service.Core.Validation.Validators.ProductCategory
{
    public class EditProductCategoryRequestValidator : AbstractValidator<UpdateProductCategoryRequest>
    {
        public EditProductCategoryRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Category description must not exceed 500 characters.");
        }
    }
}
 