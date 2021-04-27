using System;
using MicroserviceLibrary.Application.Configurations.Dispatchers;

namespace PlainClasses.Services.Person.Application.Commands.UpdatePerson
{ 
    public class UpdatePersonCommand : CommandBase
    {
        public Guid PersonId { get; }
        public Guid MilitaryRankId { get; }
        public Guid? PlatoonId { get; }
        public string Password { get; }
        public string LastName { get; }
        public string WorkPhoneNumber { get; }
        public string PersonalPhoneNumber { get; }
        public string Position { get; }

        public UpdatePersonCommand(Guid personId, Guid militaryRankId, Guid? platoonId, string password, string lastName, 
            string workPhoneNumber, string personalPhoneNumber, string position)
        {
            PersonId = personId;
            MilitaryRankId = militaryRankId;
            PlatoonId = platoonId;
            Password = password;
            LastName = lastName;
            WorkPhoneNumber = workPhoneNumber;
            PersonalPhoneNumber = personalPhoneNumber;
            Position = position;
        }
    }
}