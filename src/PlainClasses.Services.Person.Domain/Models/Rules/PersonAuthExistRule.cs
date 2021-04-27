using MicroserviceLibrary.Domain.SharedKernels;

namespace PlainClasses.Services.Person.Domain.Models.Rules
{
    public class PersonAuthExistRule : IBusinessRule
    {
        private readonly PersonAuth _personAuth;

        public PersonAuthExistRule(PersonAuth personAuth)
        {
            _personAuth = personAuth;
        }

        public bool IsBroken() => _personAuth == null;

        public string Message => $"Person auth with id: {_personAuth.Id} does not exist.";
    }
}