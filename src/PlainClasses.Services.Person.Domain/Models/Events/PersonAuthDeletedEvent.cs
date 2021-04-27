using System;
using MicroserviceLibrary.Domain.SharedKernels;

namespace PlainClasses.Services.Person.Domain.Models.Events
{
    public class PersonAuthDeletedEvent : DomainEventBase
    {
        public Guid PersonId { get; private set; }
        public Guid PersonAuthId { get; private set; }
        

        public PersonAuthDeletedEvent(Guid personId, Guid personAuthId)
        {
            PersonId = personId;
            PersonAuthId = personAuthId;
        }
    }
}