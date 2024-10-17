﻿using Bredinin.TestProject.Service.Contracts.Product.Update;
using FluentValidation;


namespace Bredinin.TestProject.Service.Core.Validation.Validators.Product
{
    public class EditProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public EditProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description is required.")
                .MaximumLength(500).WithMessage("Product description must not exceed 500 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Product price must be greater than zero.");

            RuleFor(x => x.ProductCategoryId)
                .NotEqual(Guid.Empty).WithMessage("Product category ID must be a valid GUID.");
        }
    }
}

