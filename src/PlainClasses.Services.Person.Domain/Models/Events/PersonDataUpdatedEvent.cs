using System;
using MicroserviceLibrary.Domain.SharedKernels;

namespace PlainClasses.Services.Person.Domain.Models.Events
{
    public class PersonDataUpdatedEvent : DomainEventBase
    {
        public Guid PersonId { get; private set; }

        public PersonDataUpdatedEvent(Guid personId)
        {
            PersonId = personId;
        }
    }
}