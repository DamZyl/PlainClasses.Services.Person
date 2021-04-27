using System;
using System.Collections.Generic;
using System.Linq;
using MicroserviceLibrary.Domain.SharedKernels;

namespace PlainClasses.Services.Person.Domain.Models.Rules
{
    public class PersonAuthExistsInPersonRule : IBusinessRule
    {
        private readonly IEnumerable<PersonAuth> _personAuths;
        private readonly Guid _personAuthId;

        public PersonAuthExistsInPersonRule(IEnumerable<PersonAuth> personAuths, Guid personAuthId)
        {
            _personAuths = personAuths;
            _personAuthId = personAuthId;
        } 
        
        public bool IsBroken() => _personAuths.All(x => x.Id != _personAuthId);

        public string Message => $"Person auth with id: {_personAuthId} does not in exists.";
    }
}