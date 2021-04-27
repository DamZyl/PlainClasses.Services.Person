using FluentValidation;

namespace PlainClasses.Services.Person.Application.Commands.DeleteAuth
{
    public class DeleteAuthCommandValidator : AbstractValidator<DeleteAuthCommand>
    {
        public DeleteAuthCommandValidator()
        {
            RuleFor(x => x.AuthId)
                .NotEmpty()
                .WithMessage("AuthId is empty.");
            
            RuleFor(x => x.PersonId)
                .NotEmpty()
                .WithMessage("PersonId is empty.");
        }
    }
}