using ClassLibraryDomain.Models;
using FluentValidation;

namespace ClassLibrary1Service.Validation
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(p => p.Nome).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(p => p.PrecoReal).GreaterThan(0).WithMessage("Preço deve ser maior que zero");
            RuleFor(p => p.Estoque).GreaterThanOrEqualTo(0).WithMessage("Estoque não pode ser negativo");
        }
    }
}