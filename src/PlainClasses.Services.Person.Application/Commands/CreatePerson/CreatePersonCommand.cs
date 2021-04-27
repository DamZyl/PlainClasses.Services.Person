using System;
using MicroserviceLibrary.Application.Configurations.Dispatchers;

namespace PlainClasses.Services.Person.Application.Commands.CreatePerson
{
    public class CreatePersonCommand : CommandBase<ReturnPersonViewModel>
    {
        public string PersonalNumber { get; }
        public Guid MilitaryRankId { get; }
        public Guid? PlatoonId { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string FatherName { get; }
        public DateTime BirthDate { get; }
        public string WorkPhoneNumber { get; }
        public string PersonalPhoneNumber { get; }
        public string Position { get; }

        public CreatePersonCommand(string personalNumber, Guid militaryRankId, Guid? platoonId, string password, 
            string firstName, string lastName, string fatherName, DateTime birthDate, string workPhoneNumber, 
            string personalPhoneNumber, string position)
        {
            PersonalNumber = personalNumber;
            MilitaryRankId = militaryRankId;
            PlatoonId = platoonId;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            FatherName = fatherName;
            BirthDate = birthDate;
            WorkPhoneNumber = workPhoneNumber;
            PersonalPhoneNumber = personalPhoneNumber;
            Position = position;
        }
    }
}