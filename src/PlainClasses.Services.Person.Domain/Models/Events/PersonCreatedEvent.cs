using System;
using MicroserviceLibrary.Domain.SharedKernels;

namespace PlainClasses.Services.Person.Domain.Models.Events
{
    public class PersonCreatedEvent : DomainEventBase
    {
        public Guid PersonId { get; private set; }

        public PersonCreatedEvent(Guid personId)
        {
            PersonId = personId;
        }
    }
}