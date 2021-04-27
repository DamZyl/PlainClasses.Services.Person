using MicroserviceLibrary.Domain.SharedKernels;
using PlainClasses.Services.Person.Application.Queries.GetPerson;

namespace PlainClasses.Services.Person.Application.Rules
{
    public class PersonExistRule : IBusinessRule
    {
        private readonly PersonViewModelDetail _person;

        public PersonExistRule(PersonViewModelDetail person)
        {
            _person = person;
        }

        public bool IsBroken() => _person == null;

        public string Message => $"Person with id: {_person.Id} does not exist.";
    }
}