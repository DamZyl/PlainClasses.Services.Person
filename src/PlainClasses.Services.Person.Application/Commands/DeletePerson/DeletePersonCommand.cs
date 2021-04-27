using System;
using MicroserviceLibrary.Application.Configurations.Dispatchers;

namespace PlainClasses.Services.Person.Application.Commands.DeletePerson
{
    public class DeletePersonCommand : CommandBase
    {
        public Guid PersonId { get; set; }

        public DeletePersonCommand(Guid personId)
        {
            PersonId = personId;
        }
    }
}