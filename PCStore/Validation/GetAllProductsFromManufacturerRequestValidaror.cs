using PCStore.Models.Requests;
using FluentValidation;

namespace PCStore.Validation
{
    public class GetAllProductsFromManufacturerRequestValidaror : AbstractValidator
        <GetAllProductsFromManufacturerRequest>
    {
        public GetAllProductsFromManufacturerRequestValidaror()
        {
            RuleFor(x => x.ManufacturerId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Manufacturer ID is required.");
        }
    }
}
