using System.Text.RegularExpressions;
using FluentValidation;
using PlainClasses.Services.Person.Domain.Utils;

namespace PlainClasses.Services.Person.Application.Commands.CreatePerson
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.PersonalNumber)
                .NotEmpty()
                .WithMessage("PersonalNumber is empty.");
            RuleFor(x => x.PersonalNumber)
                .Length(11)
                .WithMessage("PersonalNumber should have 11 digits.");
            
            RuleFor(x => x.MilitaryRankId)
                .NotEmpty()
                .WithMessage("MilitaryRankId is empty.");
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is empty.");
            RuleFor(x => x.Password)
                .MinimumLength(8)
                .WithMessage("Password should have 8 letters.");
            
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("FirstName is empty.");
            
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName is empty.");
            
            RuleFor(x => x.FatherName)
                .NotEmpty()
                .WithMessage("FatherName is empty.");
            
            RuleFor(x => x.PersonalPhoneNumber)
                .NotEmpty()
                .WithMessage("PersonalPhoneNumber is empty.");
            RuleFor(x => x.PersonalPhoneNumber)
                .Must(CheckPhoneNumberFormat)
                .WithMessage("PersonalPhoneNumber should have format 'xxx-xxx-xxx'.");
            
            RuleFor(x => x.WorkPhoneNumber)
                .NotEmpty()
                .WithMessage("WorkPhoneNumber is empty");
            RuleFor(x => x.WorkPhoneNumber)
                .Must(CheckPhoneNumberFormat)
                .WithMessage("WorkPhoneNumber should have format 'xxx-xxx-xxx'.");
            RuleFor(x => x.Position)
                .NotEmpty()
                .WithMessage("Position is empty.");
        }

        private static bool CheckPhoneNumberFormat(string number)
        {
            var regex = new Regex(Consts.PhoneNumberRegex);

            return regex.IsMatch(number);
        }
    }
}