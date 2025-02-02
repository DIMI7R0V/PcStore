using FluentValidation;
using PCStore.Models.Models;

namespace PCStore.Validation
{
    public class ManufacturerValidator : AbstractValidator<Manufacturer>
    {
        public ManufacturerValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Manufacturer ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Manufacturer name is required.")
                .MinimumLength(2).WithMessage("Manufacturer name must be at least 2 characters long.");
        }
    }
}
