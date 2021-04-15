using Acme.BookStore.Books;
using FluentValidation;

namespace Acme.BookStore.Validators
{
    public class CreateUpdateBookDtoValidator : AbstractValidator<CreateUpdateBookDto>
    {
        public CreateUpdateBookDtoValidator()
        {
            RuleFor((x => x.Name))
                .Length(3, 10).WithMessage("name should be [3,10]");
        }
    }
}