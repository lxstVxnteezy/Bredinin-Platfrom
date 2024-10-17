using Bredinin.TestProject.Service.Core.Validation.Validators.Product;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bredinin.TestProject.Service.Core.Validation
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidation
            (this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();
            services.AddFluentValidationAutoValidation();
            return services;
        }
    }
}
