using System;
using System.Collections.Generic;
using System.Linq;
using MicroserviceLibrary.Domain.SharedKernels;
using PlainClasses.Services.Person.Domain.Models.DomainServices;
using PlainClasses.Services.Person.Domain.Models.Enums;
using PlainClasses.Services.Person.Domain.Models.Events;
using PlainClasses.Services.Person.Domain.Models.Rules;
using PlainClasses.Services.Person.Domain.Utils.Extensions;

namespace PlainClasses.Services.Person.Domain.Models
{
    public class Person : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string PersonalNumber { get; private set; }
        public Guid MilitaryRankId { get; private set; }
        public Guid? PlatoonId { get; private set; }
        public string MilitaryRankAcr { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FatherName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string WorkPhoneNumber { get; private set; }
        public string PersonalPhoneNumber { get; private set; }
        public PersonPosition Position { get; private set; }
        private ISet<PersonAuth> _personAuths = new HashSet<PersonAuth>();
        public IEnumerable<PersonAuth> PersonAuths => _personAuths;

        #region Ef_Config

        public MilitaryRank MilitaryRank { get; set; }

        #endregion

        private Person() { }
        
        private Person(MilitaryRank militaryRank, Guid? platoonId, string personalNumber, string password, string firstName, 
            string lastName, string fatherName, DateTime birthDate, string workPhoneNumber, string personalPhoneNumber, string position)
        {
            Id = Guid.NewGuid();
            PersonalNumber = personalNumber;
            MilitaryRankId = militaryRank.Id;
            PlatoonId = platoonId;
            MilitaryRankAcr = militaryRank.Acronym;
            Password = password;
            FirstName = firstName.ToUppercaseFirstInvariant();
            LastName = lastName.ToUppercaseFirstInvariant();
            FatherName = fatherName.ToUppercaseFirstInvariant();
            BirthDate = birthDate;
            WorkPhoneNumber = workPhoneNumber;
            PersonalPhoneNumber = personalPhoneNumber;
            Position = Enum.Parse<PersonPosition>(position.ToUppercaseFirstInvariant());
            
            AddDomainEvent(new PersonCreatedEvent(Id));
        }
        
        private Person(MilitaryRank militaryRank, string personalNumber, string password, string firstName, string lastName, 
            string fatherName, DateTime birthDate, string workPhoneNumber, string personalPhoneNumber, string position)
        {
            Id = Guid.NewGuid();
            PersonalNumber = personalNumber;
            MilitaryRankId = militaryRank.Id;
            MilitaryRankAcr = militaryRank.Acronym;
            Password = password;
            FirstName = firstName.ToUppercaseFirstInvariant();
            LastName = lastName.ToUppercaseFirstInvariant();
            FatherName = fatherName.ToUppercaseFirstInvariant();
            BirthDate = birthDate;
            WorkPhoneNumber = workPhoneNumber;
            PersonalPhoneNumber = personalPhoneNumber;
            Position = Enum.Parse<PersonPosition>(position.ToUppercaseFirstInvariant());
            
            AddDomainEvent(new PersonCreatedEvent(Id));
        }

        public static Person CreatePerson(Guid militaryRankId, Guid? platoonId, string personalNumber, string password, 
            string firstName, string lastName, string fatherName, DateTime birthDate, string workPhoneNumber, 
            string personalPhoneNumber, string position, IPersonPasswordHasher passwordHasher, 
            IGetMilitaryRankForId getMilitaryRankForId)
        {
            var militaryRank = getMilitaryRankForId.Get(militaryRankId);
            CheckRule(new MilitaryRankExistRule(militaryRank));

            if (platoonId == null)
                return new Person(militaryRank, personalNumber, passwordHasher.Hash(password), firstName, lastName,
                    fatherName, birthDate, workPhoneNumber, personalPhoneNumber, position);

            return new Person(militaryRank, platoonId, personalNumber, passwordHasher.Hash(password), firstName, lastName, 
                fatherName, birthDate, workPhoneNumber, personalPhoneNumber, position);
        }

        public void UpdatePersonalData(Guid militaryRankId, Guid? platoonId, string password, string lastName,
            string workPhoneNumber, string personalPhoneNumber, string position, IPersonPasswordHasher passwordHasher, 
            IGetMilitaryRankForId getMilitaryRankForId)
        {
            var militaryRank = getMilitaryRankForId.Get(militaryRankId);
            CheckRule(new MilitaryRankExistRule(militaryRank));

            if (MilitaryRankId != militaryRankId)
            {
                MilitaryRankId = militaryRankId;
                MilitaryRankAcr = militaryRank.Acronym;
            }

            if (PlatoonId != platoonId)
            {
                PlatoonId = platoonId;
            }

            if (!passwordHasher.Check(Password, password))
            {
                Password = passwordHasher.Hash(password);
            }

            if (LastName != lastName.ToUppercaseFirstInvariant())
            {
                LastName = lastName;
            }

            if (WorkPhoneNumber != workPhoneNumber)
            {
                WorkPhoneNumber = workPhoneNumber;
            }

            if (PersonalPhoneNumber != personalPhoneNumber)
            {
                PersonalPhoneNumber = personalPhoneNumber;
            }

            if (Enum.IsDefined(typeof(PersonPosition), position) && Position.ToString() != position.ToUppercaseFirstInvariant())
            {
                Position = Enum.Parse<PersonPosition>(position);
            }
            
            AddDomainEvent(new PersonDataUpdatedEvent(Id));
        }

        public void ChangePlatoon(Guid platoonId)
        {
            PlatoonId = platoonId;
        }
        
        public void ChangePlatoon()
        {
            PlatoonId = null;
        }

        public void AddAuthToPerson(string authName)
        {
            CheckRule(new PersonAuthRule(PersonAuths, authName));

            var auth = PersonAuth.CreateAuthForPerson(Id, authName);
            _personAuths.Add(auth);
            
            AddDomainEvent(new PersonAuthAddedEvent(Id, authName));
        }
        
        public void DeleteAuthFromPerson(Guid authId, IGetPersonAuthForId getPersonAuthForId)
        {
            var personAuth = getPersonAuthForId.Get(authId);
            CheckRule(new PersonAuthExistRule(personAuth));
            CheckRule(new PersonAuthExistsInPersonRule(PersonAuths, personAuth.Id));
            
            _personAuths.Remove(_personAuths.Single(x => x.Id == personAuth.Id));
            
            AddDomainEvent(new PersonAuthDeletedEvent(Id, personAuth.Id));
        }
    }
}