using FluentValidation;
using Product.Application.Products.Commands.UpdateProduct;

namespace Product.Application.Products.Commands.UpdateProductCommandValidator;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ürün adı boş olamaz.")
            .MaximumLength(200).WithMessage("Ürün adı 200 karakterden uzun olamaz.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .MaximumLength(2000).WithMessage("Açıklama 2000 karakterden uzun olamaz.");

        RuleFor(x => x.Brand)
            .MaximumLength(100).WithMessage("Marka adı 100 karakterden uzun olamaz.");
    }
}
