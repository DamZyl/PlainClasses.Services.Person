using System;
using MicroserviceLibrary.Application.Configurations.Dispatchers;

namespace PlainClasses.Services.Person.Application.Queries.GetPerson
{
    public class GetPersonQuery : IQuery<PersonViewModelDetail>
    {
        public Guid Id { get; set; }
    }
}