using System.Collections.Generic;
using System.Linq;
using MicroserviceLibrary.Domain.SharedKernels;

namespace PlainClasses.Services.Person.Domain.Models.Rules
{
    public class PersonAuthRule : IBusinessRule
    {
        private readonly IEnumerable<PersonAuth> _personAuths;
        private readonly string _authName;

        public PersonAuthRule(IEnumerable<PersonAuth> personAuths, string authName)
        {
            _personAuths = personAuths;
            _authName = authName;
        }

        public bool IsBroken() => _personAuths.Any(x => x.AuthName == _authName);
        

        public string Message => "Person already has this permission.";
    }
}