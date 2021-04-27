using System.Collections.Generic;
using MicroserviceLibrary.Application.Configurations.Dispatchers;

namespace PlainClasses.Services.Person.Application.Queries.GetPersons
{
    public class GetPersonsQuery : IQuery<IEnumerable<PersonViewModel>> { }
}