using System;
using MicroserviceLibrary.Application.Configurations.Dispatchers;

namespace PlainClasses.Services.Person.Application.Commands.DeleteAuth
{
    public class DeleteAuthCommand : CommandBase
    {
        public Guid PersonId { get; }
        public Guid AuthId { get; }

        public DeleteAuthCommand(Guid personId, Guid authId)
        {
            PersonId = personId;
            AuthId = authId;
        }
    }
}