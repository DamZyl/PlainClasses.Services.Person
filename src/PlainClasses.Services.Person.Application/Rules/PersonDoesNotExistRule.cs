using MicroserviceLibrary.Domain.SharedKernels;

namespace PlainClasses.Services.Person.Application.Rules
{
    public class PersonDoesNotExistRule : IBusinessRule
    {
        private readonly Domain.Models.Person _person;

        public PersonDoesNotExistRule(Domain.Models.Person person)
        {
            _person = person;
        }

        public bool IsBroken() => _person == null;

        public string Message => $"Person with id: {_person.Id} does not exist.";
    }
}