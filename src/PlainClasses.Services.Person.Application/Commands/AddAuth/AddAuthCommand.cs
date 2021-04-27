using System;
using MicroserviceLibrary.Application.Configurations.Dispatchers;
using PlainClasses.Services.Person.Application.Commands.CreatePerson;

namespace PlainClasses.Services.Person.Application.Commands.AddAuth
{
    public class AddAuthCommand : CommandBase<ReturnPersonViewModel>
    {
        public Guid PersonId { get; }
        public string AuthName { get; }

        public AddAuthCommand(Guid personId, string authName)
        {
            PersonId = personId;
            AuthName = authName;
        }
    }
}