using System.Collections.Generic;
using PlainClasses.Services.Person.Application.Queries.GetPersons;

namespace PlainClasses.Services.Person.Application.Queries.GetPerson
{
    public class PersonViewModelDetail : PersonViewModel
    {
        public List<AuthViewModel> PersonAuths { get; set; }
    }
}