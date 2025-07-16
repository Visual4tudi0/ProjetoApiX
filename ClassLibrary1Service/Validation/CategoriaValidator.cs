using ClassLibraryDomain.Models;
using FluentValidation;

namespace ClassLibrary1Service.Validation
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("Nome da categoria é obrigatório");
        }
    }
}