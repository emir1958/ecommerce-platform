using FluentValidation;

namespace Product.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
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
