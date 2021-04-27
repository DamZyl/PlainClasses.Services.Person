using FluentValidation;

namespace PlainClasses.Services.Person.Application.Commands.AddAuth
{
    public class AddAuthCommandValidator : AbstractValidator<AddAuthCommand>
    {
        public AddAuthCommandValidator()
        {
            RuleFor(x => x.AuthName)
                .NotEmpty()
                .WithMessage("AuthName is empty.");
        }
    }
}