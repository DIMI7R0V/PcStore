using FluentValidation;
using PCStore.Models.Models;

namespace PCStore.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Product ID is required.");

            RuleFor(n => n.ProductName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2).WithMessage("Product must be a least 2 characters");

            RuleFor(m => m.ManufacturerId)
               .NotNull()
               .NotEmpty()
               .WithMessage("Manufacturer ID is required.");
        }
    }
}
